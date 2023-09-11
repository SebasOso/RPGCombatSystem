using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        enemyStateMachine.ragdoll.ToggleRagdoll(true);
        for (int i = 0; i < enemyStateMachine.WeaponsLogics.Count; i++)
        {
            enemyStateMachine.WeaponsLogics[i].SetActive(false);
        }
        GameObject.Destroy(enemyStateMachine.Target);
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }
}