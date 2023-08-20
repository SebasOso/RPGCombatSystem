using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTree = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardSpeed = Animator.StringToHash("targetingForward");
    private readonly int TargetingRightSpeed = Animator.StringToHash("targetingRight");

    private const float AnimatorDampTime = 0.1f;
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnCancelTarget;
        stateMachine.Animator.Play(TargetingBlendTree);
    }
    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        if(stateMachine.Targeter.currentTarget ==  null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        
        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);
        UpdateAnimator(deltaTime);
        FaceTarget();
    }

    private void UpdateAnimator(float deltaTime)
    {
        if(stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardSpeed, 0, AnimatorDampTime, deltaTime);
        }
        else
        {
            float valueForward = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingForwardSpeed, valueForward, AnimatorDampTime, deltaTime);
        }
        if(stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetingRightSpeed, 0, AnimatorDampTime, deltaTime);
        }
        else
        {
            float valueRight = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingRightSpeed, valueRight, AnimatorDampTime, deltaTime);
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnCancelTarget;
    }


    private void OnCancelTarget()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();
        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        return movement;
    }
}
