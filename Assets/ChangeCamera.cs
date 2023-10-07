using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] PlayerStateMachine player;
    [SerializeField] Animator playerAnimator;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            playerAnimator.CrossFadeInFixedTime("LocomotionBT 1", 0.1F);  
            player.IsInCameraOne = true;
        }  
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            playerAnimator.CrossFadeInFixedTime("LocomotionBT", 0.1F);    
            player.IsInCameraOne = false;
        }
    }
}
