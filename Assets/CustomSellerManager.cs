using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSellerManager : MonoBehaviour
{
    public static CustomSellerManager Instance;
    [SerializeField] GameObject interactPlayerUI;
    [SerializeField] private bool isPlayerNear = false;
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
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            interactPlayerUI.SetActive(true);
            isPlayerNear = true;
            other.GetComponent<PlayerStateMachine>().IsNearNPC = true;
            other.GetComponent<InputReader>().IsNPCInteracting = true;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            interactPlayerUI.SetActive(false);
            isPlayerNear = false;
            other.GetComponent<PlayerStateMachine>().IsNearNPC = false;
            other.GetComponent<InputReader>().IsNPCInteracting = false;
            GetComponent<Animator>().SetBool("isInteracting", false);
        }
    }
    public void OpenShop()
    {
        if(isPlayerNear)
        {
            GetComponent<Animator>().SetBool("isInteracting", true);
        }
    }
    public void CloseShop()
    {
        if(isPlayerNear)
        {
            GetComponent<Animator>().SetBool("isInteracting", false);
        }
    }
}
