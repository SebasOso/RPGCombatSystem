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
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTree, CrossFadeDuration);
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.IsTornado)
        {
            stateMachine.SwitchState(new PlayerTornadoState(stateMachine));
        }
        if(stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        Vector3 movement = CalculateMovement(deltaTime);

        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        if(stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            stateMachine.FreeLookMovementSpeed = 0f;
            if(stateMachine.IdleBreaker.isBreakTime)
            {
                stateMachine.SwitchState(new PlayerBreakState(stateMachine));
            }
            return;
        }
        if(stateMachine.InputReader.IsRunning)
        {
            stateMachine.IdleBreaker.timer = 0f;
            stateMachine.IdleBreaker.isBreakTime = false;
            stateMachine.FreeLookMovementSpeed = stateMachine.RunningMovementSpeed;
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, stateMachine.RunningMovementSpeed, AnimatorDampTime, deltaTime);
            FaceMovementDirection(movement, deltaTime);
            return;
        }
        stateMachine.FreeLookMovementSpeed = stateMachine.WalkingMovementSpeed;
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, stateMachine.WalkingMovementSpeed, AnimatorDampTime, deltaTime);
        stateMachine.IdleBreaker.isBreakTime = false;
        stateMachine.IdleBreaker.timer = 0f;
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {

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
