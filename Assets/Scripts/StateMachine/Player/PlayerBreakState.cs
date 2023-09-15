using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerBreakState : PlayerBaseState
{
    private readonly int BreakHash = Animator.StringToHash("Break");
    private Vector3 momentum;
    private const float CrossFadeDuration = 0.1f;
    private float timer = 0f;
    public PlayerBreakState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(BreakHash, CrossFadeDuration);
    }

    public override void Exit()
    {
        timer = 0f;
        stateMachine.IdleBreaker.timer = 0f;
        stateMachine.IdleBreaker.isBreakTime = false;
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        timer += deltaTime;
        if(timer >= 2f)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
