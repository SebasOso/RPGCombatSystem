using System.Collections;
using System.Collections.Generic;
using RPG.Inventories;
using UnityEngine.UI;
using RPG.Combat;
using UnityEngine;
using RPG.Saving;
using Newtonsoft.Json.Linq;
using System;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] InventoryItem item;
    [SerializeField] Image itemSprite;
    [SerializeField] string itemId = "PLATANOOOOOOOOOOOOOOOOOOOOO";
    [SerializeField] Weapon weapon;
    [SerializeField] int index;
    [SerializeField] Sprite defaultSprite;
    public void EquipWeaponFromInventory()
    {
        if(item == null){return;}
        Armory.Instance.EquipWeapon(weapon);
        Armory.Instance.DesactivateBackWeapon();
        InventoryManager.Instance.SetNewInventoryWeapon(this.item, this);
    }
    public void SetItem(int index)
    {
        this.item = MenuManager.Instance.GetItemInSlot(index);
        itemId = item?.GetItemID();
        weapon = item.GetWeapon();
        itemSprite.sprite = item?.GetIcon();
    }
    public void DeleteItemFromInventory()
    {
        this.item = null;
        this.itemId = null;
        this.weapon = null;
        this.itemSprite.sprite = defaultSprite;
        MenuManager.Instance.DeleteItemFlomSlot(index);
    }
}
