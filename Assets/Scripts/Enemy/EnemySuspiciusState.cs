using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuspiciusState : EnemyBaseState
{
    float timeWhileSuspicius;
    public EnemySuspiciusState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        timeWhileSuspicius = 0f;
    }

    public override void Tick(float deltaTime)
    {
        timeWhileSuspicius += deltaTime;
        enemyStateMachine.EnemyMover.MoveTo(Vector3.zero, 0f);
        if(enemyStateMachine.FieldOfView.canSeePlayer)
        {
            enemyStateMachine.SwitchState(new EnemyChasingState(enemyStateMachine));
        }
        else if (timeWhileSuspicius >= enemyStateMachine.SuspicionTime)
        {
            enemyStateMachine.SwitchState(new EnemyIdleState(enemyStateMachine));
        }
    }

    public override void Exit()
    {
        timeWhileSuspicius = 0f;
    }
}
