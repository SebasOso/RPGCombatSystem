using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField]
    public float RunningMovementSpeed {get; private set;}

    [field: SerializeField]
    public Animator Animator {get; private set;}

    [field: SerializeField]
    public float PlayerDetectionRange {get; private set;}

    [field: SerializeField]
    public CharacterController CharacterController {get; private set;}

    [field: SerializeField]
    public ForceReceiver ForceReceiver {get; private set;}

    public GameObject Player {get; private set;}

    private void Start() 
    {
        Player = GameManager.Instance.Player;
        SwitchState(new EnemyIdleState(this));
    }
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectionRange);
    }
}
