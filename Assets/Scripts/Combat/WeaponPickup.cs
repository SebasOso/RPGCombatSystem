using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private UIStateMachine uIStateMachine;
    [SerializeField] private Weapon weapon = null;
    [SerializeField] GameObject pickupUI;
    GameObject player;
    private InputReader InputReader;
    Weapon weaponToPick = null;
    private void Awake() 
    {
        player = GameObject.FindWithTag("Player");
        InputReader = player.GetComponent<InputReader>();
    }
    private void Start() 
    {
        
    }
    private void PickUp()
    {
        if(weaponToPick != null)
        {
            player.GetComponent<Armory>().EquipWeapon(weaponToPick);
            weaponToPick = null;
            pickupUI.SetActive(false);
            StartCoroutine(HideForSeconds(5f));
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            weaponToPick = weapon;
            pickupUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            weaponToPick = null;
            pickupUI.SetActive(false);
        }
    }
    private IEnumerator HideForSeconds(float seconds)
    {
        ToggleShowPickup(false);
        yield return new WaitForSeconds(seconds);
        ToggleShowPickup(true);
    }
 
    private void ToggleShowPickup(bool toggle)
    {
        GetComponent<Collider>().enabled = toggle;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(toggle);
        }
    }
}
