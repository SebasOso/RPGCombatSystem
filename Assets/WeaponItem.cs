using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine.UI;
using RPG.Combat;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] InventoryItem item;
    [SerializeField] Image itemSprite;
    private void OnEnable() 
    {
        if(item == null){return;}
        itemSprite.sprite = item?.GetIcon();
    }
    public void EquipWeaponFromInventory()
    {
        if(item == null){return;}
        Armory.Instance.EquipWeapon(item?.GetWeapon());
        Armory.Instance.DesactivateBackWeapon();
    }
}
