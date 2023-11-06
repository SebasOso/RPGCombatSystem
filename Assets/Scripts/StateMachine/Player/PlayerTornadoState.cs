using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTornadoState : PlayerBaseState
{
    private readonly int TornadoHash = Animator.StringToHash("Tornado");
    private readonly int LocomotionSpeed = Animator.StringToHash("speed");
    private const float CrossFadeDuration = 0.1f;
    private float duration = 1f;
    public PlayerTornadoState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(TornadoHash, CrossFadeDuration);
    }

    public override void Exit()
    {

    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        duration -= deltaTime;
        if(duration <= 0f)
        {
            ReturnToLocomotion();
            stateMachine.IsTornado = false;
        }
    }
}
