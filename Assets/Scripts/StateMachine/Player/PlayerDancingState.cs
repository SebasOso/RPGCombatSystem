using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDancingState : PlayerBaseState
{
    private readonly int DanceHash = Animator.StringToHash("Dance");
    private const float CrossFadeDuration = 0.1f;
    private float timer = 0f;
    public PlayerDancingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DanceHash, CrossFadeDuration);
    }

    public override void Exit()
    {

    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        timer += deltaTime;
        if(timer >= 21.5f)
        {
            Debug.Log("Faaaaaaaaade");
            stateMachine.EndFader.EndFade();
        }
    }

}
