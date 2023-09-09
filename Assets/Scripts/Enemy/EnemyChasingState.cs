using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int EnemyLocomotionBlendTree = Animator.StringToHash("LocomotionBT");
    private readonly int EnemyLocomotionSpeed = Animator.StringToHash("enemySpeed");
    private const float CrossFadeDuration = 0.1f;
    public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        enemyStateMachine.Animator.CrossFadeInFixedTime(EnemyLocomotionBlendTree, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if(!IsInChasingRange())
        {
            enemyStateMachine.SwitchState(new EnemyIdleState(this.enemyStateMachine));
            return;
        }
        else if(IsInAttackRange())
        {
            enemyStateMachine.SwitchState(new EnemyAttackingState(this.enemyStateMachine));
            return;
        }
        MoveToPlayer(deltaTime);
        FacePlayer();
        enemyStateMachine.Animator.SetFloat(EnemyLocomotionSpeed, 1.0f, 0.1f, deltaTime);
    }

    private void MoveToPlayer(float deltaTime)
    {
        if(enemyStateMachine.navMeshAgent.isOnNavMesh)
        {
            enemyStateMachine.navMeshAgent.destination = enemyStateMachine.Player.transform.position;
            Move(enemyStateMachine.navMeshAgent.desiredVelocity.normalized * enemyStateMachine.RunningMovementSpeed, deltaTime);
        }
        enemyStateMachine.navMeshAgent.velocity = enemyStateMachine.CharacterController.velocity;
    }

    public override void Exit()
    {
        enemyStateMachine.navMeshAgent.ResetPath();
        enemyStateMachine.navMeshAgent.velocity = Vector3.zero;
    }
}
