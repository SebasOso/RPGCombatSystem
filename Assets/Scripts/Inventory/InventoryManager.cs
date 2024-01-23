using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Inventories;
using Newtonsoft.Json.Linq;
using RPG.Combat;
using RPG.Saving;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private WeaponItem[] itemsInInventory;
    public InventoryItem weaponEquipped;
    public static InventoryManager Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start() 
    {
        Redraw();
        if(weaponEquipped == null)
        {
            weaponEquipped = Armory.Instance.currentWeapon.value.GetInventoryItem();
        }
    }
    private void OnEnable() 
    {
        Redraw();
        if (weaponEquipped == null)
        {
            weaponEquipped = Armory.Instance.currentWeapon.value.GetInventoryItem();
        }
    }
    private void Redraw()
    {
        for (int i = 0; i < MenuManager.Instance.GetSize(); i++)
        {
            if(MenuManager.Instance.GetItemInSlot(i) == null)
            {
                continue;
            }
            itemsInInventory[i].SetItem(i);
        }
    }
    private IEnumerator SetNewWeapon(InventoryItem weaponToEquip, WeaponItem weaponToDeleteFromInventory)
    {
        yield return new WaitForSeconds(0.1f);
        MenuManager.Instance.AddToFirstEmptySlot(weaponEquipped);
        weaponToDeleteFromInventory.DeleteItemFromInventory();
        weaponEquipped = weaponToEquip;
        Redraw();
    }

    //Setters
    public void SetNewInventoryWeapon(InventoryItem weaponToEquip, WeaponItem weaponToDeleteFromInventory)
    {
        StartCoroutine(SetNewWeapon(weaponToEquip, weaponToDeleteFromInventory));
    }
}
