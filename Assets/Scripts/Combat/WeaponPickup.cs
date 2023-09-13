using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponToPick = null;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Armory>().EquipWeapon(weaponToPick);
            Destroy(gameObject);
        }
    }
}
