using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.ArmRig.weight = 1f;
        stateMachine.HeadRig.weight = 1f;
    }

    public override void Tick(float deltaTime)
    {
        if(!stateMachine.InputReader.IsAiming)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
        Move(deltaTime);
        stateMachine.Animator.SetFloat("speed", 0, 0.1f, deltaTime);
    }
    public override void Exit()
    {
        stateMachine.ArmRig.weight = 0f;
        stateMachine.HeadRig.weight = 0f;
    }
}
