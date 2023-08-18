using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    private float timer = 5f;
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        Debug.Log("Enter");
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        Debug.Log("Exit");
    }

    public override void Tick(float deltaTime)
    {
        timer -= deltaTime;
        Debug.Log(timer);
        if(timer <= 0f)
        {
            //stateMachine.SwitchState(new PlayerTestState(stateMachine));
        }
    }

    private void OnJump()
    {
        Debug.Log("JUMP!!!");
        stateMachine.SwitchState(new PlayerTestState(stateMachine));
    }
}
