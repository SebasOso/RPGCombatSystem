using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RPG.Combat;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] 
    public PatrolPath PatrolPath {get; private set;}
    [field: SerializeField]
    public float StayTime {get; private set;}
    [field: SerializeField]
    public float SuspicionTime {get; private set;}
    [field: SerializeField]
    public float WaypointTolerance {get; private set;}
    [field: SerializeField] 
    public EnemyMover EnemyMover {get; private set;}
    [field: SerializeField] 
    public EnemyArmory EnemyArmory {get; private set;}
    [field: SerializeField] 
    public Ragdoll ragdoll {get; private set;}
    [field: SerializeField] 
    public List<GameObject> WeaponsLogics {get; private set;}
    [field: SerializeField] 
    public Target Target {get; private set;}
    [field: SerializeField] 
    public Health Health {get; private set;}
    [field: SerializeField]
    public NavMeshAgent navMeshAgent {get; private set;}

    [field: SerializeField]
    public float RunningMovementSpeed {get; private set;}

    [field: SerializeField]
    public Animator Animator {get; private set;}

    [field: SerializeField]
    public float PlayerDetectionRange {get; private set;}
    public Health Player {get; private set;}

    private void Start() 
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        SwitchState(new EnemyIdleState(this));
    }
    private void OnEnable() 
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }
    private void OnDisable() 
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectionRange);
    }
    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
    }
    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }
}
