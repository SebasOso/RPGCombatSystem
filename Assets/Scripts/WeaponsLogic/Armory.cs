using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RPG.Combat;
using RPG.Saving;
using RPG.Stats;
using RPG.Utils;
using UnityEngine;
using UnityEngine.Rendering;

public class Armory : MonoBehaviour, IJsonSaveable, IModifierProvider
{
    [Header("Weapons")]
    [SerializeField] public Weapon defaultWeapon;
    [SerializeField] private Transform rightHandSocket;
    [SerializeField] private Transform leftHandSocket;
    public WeaponPickup weaponToPickUp;
    public float damage = 0f;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    public LazyValue<Weapon> currentWeapon;
    [SerializeField] Targeter Targeter;
    private void Awake() 
    {
        currentWeapon = new LazyValue<Weapon>(GetInitialWeapon);
    }
    private Weapon GetInitialWeapon()
    {
        AttachWeapon(defaultWeapon);
        return defaultWeapon;
    }
    private void Start() 
    {
        currentWeapon.ForceInit();
        animator.SetFloat("attackSpeed", GetComponent<BaseStats>().GetStat(Stat.AttackSpeed));
        damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
    }
    private void OnEnable() 
    {
        GetComponent<BaseStats>().OnLevelUP += UpdateAS;
        GetComponent<BaseStats>().OnLevelUP += UpdateDamage;
    }
    private void OnDisable() 
    {
        GetComponent<BaseStats>().OnLevelUP -= UpdateAS;
        GetComponent<BaseStats>().OnLevelUP -= UpdateDamage;
    }
    private void UpdateDamage()
    {
        damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
    }

    private void UpdateAS()
    {
        animator.SetFloat("attackSpeed", GetComponent<BaseStats>().GetStat(Stat.AttackSpeed));
    }

    public void EquipWeapon(Weapon weapon)
    {
        currentWeapon.value = weapon;
        AttachWeapon(weapon);
        damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
    }

    private void AttachWeapon(Weapon weapon)
    {
        weapon.Spawn(rightHandSocket, leftHandSocket, animator);
        GetComponent<InputReader>().CanRuneAttack = weapon.CanRuneAttack;
    }

    public void PickUpWeapon()
    {
        weaponToPickUp?.PickUp();
        GetComponent<InputReader>().IsInteracting = false;
        Destroy(weaponToPickUp.gameObject);
    }
    public JToken CaptureAsJToken()
    {
        return JToken.FromObject(currentWeapon.value.name);
    }
    void Shoot()
    {
        if (Targeter.currentTarget == null || Targeter.currentTarget.GetComponent<Health>().IsDead()) return;
        if(currentWeapon.value.HasProjectile())
        {
            currentWeapon.value.LaunchProjectile(rightHandSocket,leftHandSocket,Targeter.currentTarget.GetComponent<Health>(), damage);
        }
    }
    public void RestoreFromJToken(JToken state)
    {
        string weaponName = state.ToObject<string>();
        Weapon weapon = Resources.Load<Weapon>(weaponName);
        EquipWeapon(weapon);
    }

    public IEnumerable<float> GetAdditiveModifier(Stat stat)
    {
        if(stat == Stat.Damage)
        {
            yield return currentWeapon.value.GetWeaponDamage();
        }
    }

    public IEnumerable<float> GetPercentageModifier(Stat stat)
    {
        if(stat == Stat.Damage)
        {
            yield return currentWeapon.value.GetPercentageDamage();
        }
    }
}
