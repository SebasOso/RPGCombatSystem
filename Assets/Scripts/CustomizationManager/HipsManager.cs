using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipsManager : MonoBehaviour, IEnumerator
{
    public static HipsManager Instance;
    [SerializeField] private GameObject mainAccesories;
    [SerializeField] private List<GameObject> accesoriesList = new List<GameObject>();
    [SerializeField] int position = -1;

    public object Current => CurrentAccesorie();
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
        Reset();
    }
    public bool MoveNext()
    {
        if(position >= 0 && position < accesoriesList.Count-1)
        {
            position ++;
            accesoriesList[position].SetActive(true);
            accesoriesList[position-1].SetActive(false);
        }
        return position < accesoriesList.Count;
    }
    public bool MoveBack()
    {
        if(position > 0)
        {
            position --;
            accesoriesList[position].SetActive(true);
            accesoriesList[position+1].SetActive(false);
        }
        return position < accesoriesList.Count;
    }
    public void Reset()
    {
        position = 0;
        foreach (GameObject accesorie in accesoriesList)
        {
            accesorie.SetActive(false);
        }
        accesoriesList[position].SetActive(true);
    }
    public GameObject CurrentAccesorie()
    {
        try
        {
            return accesoriesList[position];
        }
        catch (IndexOutOfRangeException)
        {
            throw new InvalidOperationException();
        }
    }
}
