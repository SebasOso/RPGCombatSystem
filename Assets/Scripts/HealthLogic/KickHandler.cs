using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickHandler : MonoBehaviour
{
    [SerializeField] private GameObject kickLogic;

    public void EnableKick()
    {
        kickLogic.SetActive(true);
    }
    public void DisableKick()
    {
        kickLogic.SetActive(false);
    }
}
