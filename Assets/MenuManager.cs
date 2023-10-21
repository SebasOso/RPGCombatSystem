using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using Newtonsoft.Json.Linq;
using RPG.Saving;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private InventoryItem[] slots;
    [SerializeField] private GameObject inventoryCanvasGO;
    [SerializeField] private GridLayoutGroup contentInventory;
    [SerializeField] private PlayerStateMachine playerStateMachine;
    public bool isPaused;
    // Start is called before the first frame update
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {   
        inventoryCanvasGO.SetActive(false);
        slots = new InventoryItem[24];
    }
    private void Update() 
    {
        if(InputReader.Instance.InventoryOpenCloseInput)
        {
            if(!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }    
    }
    private void Pause()
    {
        isPaused = true;
        playerStateMachine.enabled = false;
        OpenInventory();
    }
    private void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;
        playerStateMachine.enabled = true;
        CloseAllMenus();
    }
    private void OpenInventory()
    {
        inventoryCanvasGO.SetActive(true);
        EventSystem.current.SetSelectedGameObject(slots[0].GetComponent<GameObject>());
    }
    private void CloseAllMenus()
    {
        inventoryCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    private int FindEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
    private int FindSlot(InventoryItem item)
    {
        return FindEmptySlot();
    }
    public bool HasSpaceFor(InventoryItem item)
    {
        return FindSlot(item) >= 0;
    }
    public bool HasItem(InventoryItem item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (object.ReferenceEquals(slots[i], item))
            {
                return true;
            }
        }
        return false;
    }
    public bool AddToFirstEmptySlot(InventoryItem item)
    {
        int i = FindSlot(item);

        if (i < 0)
        {
            return false;
        }

        slots[i] = item;
        return true;
    }
}
