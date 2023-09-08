using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int EnemyLocomotionBlendTree = Animator.StringToHash("LocomotionBT");
    private readonly int EnemyLocomotionSpeed = Animator.StringToHash("enemySpeed");
    private const float CrossFadeDuration = 0.1f;
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {

    }

    public override void Enter()
    {
        enemyStateMachine.Animator.CrossFadeInFixedTime(EnemyLocomotionBlendTree, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if(IsInChasingRange())
        {
            Debug.Log("In Range");
            enemyStateMachine.SwitchState(new EnemyChasingState(this.enemyStateMachine));
            return;
        }
        enemyStateMachine.Animator.SetFloat(EnemyLocomotionSpeed, 0.0f, 0.1f, deltaTime);
    }

    public override void Exit()
    {
        
    }
}
