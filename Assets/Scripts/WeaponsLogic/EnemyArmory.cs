using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RPG.Combat;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

public class EnemyArmory : MonoBehaviour, IJsonSaveable
{
    [Header("Weapons")]
    [SerializeField] public Weapon defaultWeapon = null;
    [SerializeField] private Transform rightHandSocket;
    [SerializeField] private Transform leftHandSocket;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    public Weapon currentWeapon = null;
    Health Player;
    private void Awake() 
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Health>();
        if(currentWeapon == null)
        {
            EquipWeapon(defaultWeapon);
        }
    }
    private void Start() 
    {
        animator.SetFloat("attackSpeed", GetComponent<BaseStats>().GetAS());
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
        if (Player == null || Player.IsDead()) return;
        if(currentWeapon.HasProjectile())
        {
            currentWeapon.LaunchProjectile(rightHandSocket,leftHandSocket,Player);
        }
    }
    public void RestoreFromJToken(JToken state)
    {
        string weaponName = state.ToObject<string>();
        Weapon weapon = Resources.Load<Weapon>(weaponName);
        EquipWeapon(weapon);
    }
}
