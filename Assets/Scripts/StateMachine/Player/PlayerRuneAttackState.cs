using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuneAttackState : PlayerBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("RuneAttack");
    private readonly int LocomotionSpeed = Animator.StringToHash("speed");
    private const float CrossFadeDuration = 0.1f;
    private float duration = 2.41f;
    public PlayerRuneAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.InputReader.IsInRuneAttack = true;
        Move(deltaTime);
        duration -= deltaTime;
        if(duration <= 0f)
        {
            ReturnToLocomotion();
        }
    }
    public override void Exit()
    {
        stateMachine.InputReader.IsInRuneAttack = false;
    }
}
