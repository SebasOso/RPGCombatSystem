using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager Instance;
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
    public void CameraShake(CinemachineImpulseSource cinemachineImpulseSource, float impulseForce)
    {
        cinemachineImpulseSource.GenerateImpulseWithForce(impulseForce);
    }
}
