using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RPG.Combat;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

public class Armory : MonoBehaviour, IJsonSaveable
{
    [Header("Weapons")]
    [SerializeField] public Weapon defaultWeapon;
    [SerializeField] private Transform rightHandSocket;
    [SerializeField] private Transform leftHandSocket;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    public Weapon currentWeapon = null;
    [SerializeField] Targeter Targeter;
    private void Awake() 
    {
        if(currentWeapon == null)
        {
            EquipWeapon(defaultWeapon);
        }
    }
    private void Start() 
    {
        animator.SetFloat("attackSpeed", GetComponent<BaseStats>().GetStat(Stat.AttackSpeed));
        GetComponent<BaseStats>().OnLevelUp += UpdateAS;
    }

    private void UpdateAS()
    {
        animator.SetFloat("attackSpeed", GetComponent<BaseStats>().GetStat(Stat.AttackSpeed));
    }

    public void EquipWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        weapon.Spawn(rightHandSocket, leftHandSocket, animator);
    }
    public JToken CaptureAsJToken()
    {
        return JToken.FromObject(currentWeapon.name);
    }
    void Shoot()
    {
        if (Targeter.currentTarget == null || Targeter.currentTarget.GetComponent<Health>().IsDead()) return;
        if(currentWeapon.HasProjectile())
        {
            currentWeapon.LaunchProjectile(rightHandSocket,leftHandSocket,Targeter.currentTarget.GetComponent<Health>());
        }
    }
    public void RestoreFromJToken(JToken state)
    {
        string weaponName = state.ToObject<string>();
        Weapon weapon = Resources.Load<Weapon>(weaponName);
        EquipWeapon(weapon);
    }
}
