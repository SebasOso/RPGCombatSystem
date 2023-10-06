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
        stateMachine.InputReader.CancelTargetEvent += OnCancelTarget;
    }

    private void OnCancelTarget()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
    }
    public override void Exit()
    {
        stateMachine.ArmRig.weight = 0f;
    }
}
