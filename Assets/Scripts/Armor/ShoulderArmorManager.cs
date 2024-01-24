using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine.Rendering;
using RPG.Combat;

public class ShoulderArmorManager : MonoBehaviour, IJsonSaveable
{
    public static ShoulderArmorManager Instance;
    [Header("Position")]
    [SerializeField] public int shouldersPosition = 0;
    [SerializeField] public Shoulder shoulder;

    [Header("Shoulder Right")]
    [SerializeField] private GameObject mainAccesories;
    [SerializeField] private List<GameObject> accesoriesList = new List<GameObject>();

    [Header("Shoulders Left")]
    [SerializeField] private GameObject mainAccesories02;
    [SerializeField] private List<GameObject> accesoriesList02 = new List<GameObject>();
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
    void Start()
    {
        SetUpLeft();
        SetUpRight();
    }
    private void SetUpRight()
    {
        int childCount = mainAccesories.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = mainAccesories.transform.GetChild(i);
            GameObject gameObject = child.gameObject;
            if (!accesoriesList.Contains(gameObject))
            {
                accesoriesList.Add(gameObject);
            }
        }
        foreach (GameObject accesorie in accesoriesList)
        {
            accesorie.SetActive(false);
        }
        accesoriesList[shouldersPosition].SetActive(true);
    }
    private void SetUpLeft()
    {
        int childCount = mainAccesories02.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = mainAccesories02.transform.GetChild(i);
            GameObject gameObject = child.gameObject;
            if (!accesoriesList02.Contains(gameObject))
            {
                accesoriesList02.Add(gameObject);
            }
        }
        foreach (GameObject accesorie in accesoriesList02)
        {
            accesorie.SetActive(false);
        }
        accesoriesList02[shouldersPosition].SetActive(true);
    }
    public void SetShoulders(int index)
    {
        foreach (GameObject accesorie in accesoriesList)
        {
            accesorie.SetActive(false);
        }
        foreach (GameObject accesorie in accesoriesList02)
        {
            accesorie.SetActive(false);
        }
        accesoriesList02[index].SetActive(true);
        accesoriesList[index].SetActive(true);
    }
    public JToken CaptureAsJToken()
    {
        if(shoulder == null)
        {
            return JToken.FromObject(0);
        }
        else
        {
            return JToken.FromObject(shoulder.name);
        }
    }

    public void RestoreFromJToken(JToken state)
    {
        string shoulderName = state.ToObject<string>();
        Shoulder shoulder = Resources.Load<Shoulder>(shoulderName);
        this.shoulder = shoulder;
        shouldersPosition = shoulder.GetIndex();
    }
}
