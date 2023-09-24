using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchHandler : MonoBehaviour
{
    [SerializeField] private GameObject punchLogic;

    public void EnablePunch()
    {
        punchLogic.SetActive(true);
    }
    public void DisablePunch()
    {
        punchLogic.SetActive(false);
    }
}
