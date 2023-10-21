using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    public void EquipWeaponFromInventory()
    {
        Armory.Instance.EquipWeapon(weapon);
        Armory.Instance.DesactivateBackWeapon();
    }
}
