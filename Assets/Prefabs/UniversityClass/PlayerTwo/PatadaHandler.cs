using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatadaHandler : MonoBehaviour
{
    [SerializeField] private GameObject patadaLogic;

    public void EnablePatada()
    {
        patadaLogic.SetActive(true);
    }
    public void DisablePatada()
    {
        patadaLogic.SetActive(false);
    }
}
