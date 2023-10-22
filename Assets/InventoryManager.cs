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
    private void Start() 
    {
        Redraw();
    }
    private void OnEnable() 
    {
        Redraw();
    }
    private void Redraw()
    {
        for (int i = 0; i < MenuManager.Instance.GetSize(); i++)
        {
            if(MenuManager.Instance.GetItemInSlot(i) == null)
            {
                return;
            }
            itemsInInventory[i].SetItem(i);
        }
    }
}
