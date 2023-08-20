using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHandler : MonoBehaviour
{
    [SerializeField] private GameObject axeLogic;

    public void EnableAxe()
    {
        axeLogic.SetActive(true);
    }
    public void DisableAxe()
    {
        axeLogic.SetActive(false);
    }
}
