using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("speed");
    private readonly int FreeLookBlendTree = Animator.StringToHash("LocomotionBT");

    private const float AnimatorDampTime = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.InputReader.AttackEvent += OnAttack;
        stateMachine.InputReader.HeavyAttackEvent += OnHeavyAttack;
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.Animator.Play(FreeLookBlendTree);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        if(stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.AttackEvent -= OnAttack;
        stateMachine.InputReader.HeavyAttackEvent -= OnHeavyAttack;
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private void OnTarget()
    {
        if(!stateMachine.Targeter.SelectTarget()){return;}
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
    private Vector3 CalculateMovement()
    {
        Vector3 cameraForward;
        Vector3 cameraRight;

        cameraForward = stateMachine.MainCameraTransform.forward;
        cameraRight = stateMachine.MainCameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        return cameraForward * stateMachine.InputReader.MovementValue.y + cameraRight * stateMachine.InputReader.MovementValue.x;
    }
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation, 
            Quaternion.LookRotation(movement), 
            deltaTime * stateMachine.RotationDamping);
    }
    private void OnAttack()
    {
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
    }
    private void OnHeavyAttack()
    {
        stateMachine.SwitchState(new PlayerHeavyAttackState(stateMachine));
    }
}
