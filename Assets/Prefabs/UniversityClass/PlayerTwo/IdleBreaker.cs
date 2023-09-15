using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleBreaker : MonoBehaviour
{
    [SerializeField]
    private float breakTime = 5f;
    [SerializeField]
    public float timer = 0;
    [SerializeField]
    private Animator animator;
    public bool isBreakTime;
    private void Update()
    {
        IdleLogic();
    }
    private void IdleLogic()
    {
        timer += Time.deltaTime;
        if (timer >= breakTime)
        {
            isBreakTime = true;
        }
    }
}






