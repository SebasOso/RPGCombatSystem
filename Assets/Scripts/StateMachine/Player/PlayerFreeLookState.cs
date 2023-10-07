using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("speed");
    private readonly int FreeLookBlendTree = Animator.StringToHash("LocomotionBT");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        if(stateMachine.IsInCameraOne)
        {
            stateMachine.Animator.CrossFadeInFixedTime("LocomotionBT 1", CrossFadeDuration);
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTree, CrossFadeDuration);
        }
    }
    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement(deltaTime);

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        if(stateMachine.InputReader.IsAiming)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        if(stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            stateMachine.FreeLookMovementSpeed = 0f;
            return;
        }
        if(stateMachine.InputReader.IsRunning)
        {
            stateMachine.FreeLookMovementSpeed = stateMachine.RunningMovementSpeed;
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, stateMachine.RunningMovementSpeed, AnimatorDampTime, deltaTime);
            FaceMovementDirection(movement, deltaTime);
            return;
        }
        stateMachine.FreeLookMovementSpeed = stateMachine.WalkingMovementSpeed;
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, stateMachine.WalkingMovementSpeed, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
    }
    private Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 cameraForward = stateMachine.MainCameraTransform.forward;
        Vector3 cameraRight = stateMachine.MainCameraTransform.right;

        cameraForward.y = 0f;  
        
        cameraForward.Normalize();
            
        Vector3 movement = cameraForward * stateMachine.InputReader.MovementValue.y + cameraRight * stateMachine.InputReader.MovementValue.x;

        return movement;
    }
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation, 
            Quaternion.LookRotation(movement), 
            deltaTime * stateMachine.RotationDamping);
    }
}
