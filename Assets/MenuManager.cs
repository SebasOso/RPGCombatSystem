using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RPG.Saving;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private GameObject inventoryCanvasGO;
    [SerializeField] private GridLayoutGroup contentInventory;
    [SerializeField] private List<GameObject> itemsInventory = new List<GameObject>();
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
        int childCount = contentInventory.transform.childCount;
        if(childCount <= 0)
        {
            return;
        }
        for (int i = 0; i < childCount; i++)
        {
            Transform child = contentInventory.transform.GetChild(i);
            GameObject gameObject = child.gameObject;
            if(!itemsInventory.Contains(gameObject))
            {
                itemsInventory.Add(gameObject);
            }
        }
        EventSystem.current.SetSelectedGameObject(itemsInventory[0]);
    }
    private void CloseAllMenus()
    {
        inventoryCanvasGO.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
