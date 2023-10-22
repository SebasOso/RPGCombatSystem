using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Inventories;
using Newtonsoft.Json.Linq;
using RPG.Saving;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IJsonSaveable
{
    public static MenuManager Instance;
    [SerializeField] private InventoryItem[] slots;
    [SerializeField] private GameObject inventoryCanvasGO;
    [SerializeField] private GridLayoutGroup contentInventory;
    [SerializeField] private List<GameObject> itemsGO;
    [SerializeField] private PlayerStateMachine playerStateMachine;
    [SerializeField] string a;
    public bool isPaused;
    public event Action inventoryUpdated;
    // Start is called before the first frame update
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        inventoryCanvasGO.SetActive(false);
        slots = new InventoryItem[24];
    }
    public int GetSize()
    {
        return slots.Length;
    }
    void Start()
    {   
        int childCount = contentInventory.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = contentInventory.transform.GetChild(i);
            GameObject gameObject = child.gameObject;
            if (!itemsGO.Contains(gameObject))
            {
                itemsGO.Add(gameObject);
            }
        }
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
    public static MenuManager GetPlayerInventory()
    {
        var player = GameObject.FindWithTag("Player");
        return player.GetComponent<MenuManager>();
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
        EventSystem.current.SetSelectedGameObject(itemsGO[0]);
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
    public InventoryItem GetItemInSlot(int slot)
    {
        return slots[slot];
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

    public JToken CaptureAsJToken()
    {
        var slotStrings = new string[24];
        for (int i = 0; i < 24; i++)
        {
            if (slots[i] != null)
            {
                slotStrings[i] = slots[i].GetItemID();
            }
        }
        return JToken.FromObject(slotStrings);
    }

    public void RestoreFromJToken(JToken state)
    {
        var slotStrings = state.ToObject<String[]>();
        for (int i = 0; i < 24; i++)
        {
            slots[i] = InventoryItem.GetFromID(slotStrings[i]);
        }
        if (inventoryUpdated != null)
        {
            inventoryUpdated();
        }
    }
}
