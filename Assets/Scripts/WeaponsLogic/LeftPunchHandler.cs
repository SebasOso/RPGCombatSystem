using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPunchHandler : MonoBehaviour
{
    [SerializeField] private GameObject leftPunchLogic;

    public void EnableLeftPunch()
    {
        leftPunchLogic.SetActive(true);
    }
    public void DisableLeftPunch()
    {
        leftPunchLogic.SetActive(false);
    }
}
