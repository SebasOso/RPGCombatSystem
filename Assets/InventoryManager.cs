using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] GameObject inventoryGO;
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void AddItemToInventory(GameObject ItemToAdd)
    {
        if(ItemToAdd != null)
        {
            GameObject Item = Instantiate(ItemToAdd, inventoryGO.transform);
        }
    }
}
