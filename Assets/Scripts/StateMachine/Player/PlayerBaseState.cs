using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }
    protected void FaceTarget()
    {
        if(stateMachine.Targeter.currentTarget == null){return;}
        Vector3 targetDirection = (stateMachine.Targeter.currentTarget.transform.position - stateMachine.transform.position);
        targetDirection.y = 0;
        stateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);
    }
}
