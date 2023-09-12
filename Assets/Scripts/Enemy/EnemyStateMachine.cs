using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] 
    public Ragdoll ragdoll {get; private set;}
    [field: SerializeField] 
    public List<GameObject> WeaponsLogics {get; private set;}
    [field: SerializeField] 
    public Target Target {get; private set;}
    [field: SerializeField] 
    public Health Health {get; private set;}
    [field: SerializeField] 
    public Weapon DefaultWeapon {get; private set;}

    [field: SerializeField] 
    public Transform RightHandSocket {get; private set;}

    [field: SerializeField] 
    public Transform LeftHandSocket {get; private set;}

    [field: SerializeField]
    public NavMeshAgent navMeshAgent {get; private set;}

    [field: SerializeField]
    public float RunningMovementSpeed {get; private set;}

    [field: SerializeField]
    public Animator Animator {get; private set;}

    [field: SerializeField]
    public float PlayerDetectionRange {get; private set;}

    [field: SerializeField]
    public float AttackRange {get; private set;}

    [field: SerializeField]
    public CharacterController CharacterController {get; private set;}

    [field: SerializeField]
    public ForceReceiver ForceReceiver {get; private set;}

    public GameObject Player {get; private set;}
    public Weapon currentWeapon {get; private set;}

    private void Start() 
    {
        EquipWeapon(DefaultWeapon);
        Player = GameManager.Instance.Player;
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;
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
    public void EquipWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        weapon.Spawn(RightHandSocket, LeftHandSocket, Animator);
        AttackRange = currentWeapon.GetWeaponRange();
    }
    void Shoot()
    {
        if(Player == null){return;}
        if(currentWeapon.HasProjectile())
        {
            currentWeapon.LaunchProjectile(RightHandSocket,LeftHandSocket,Player.GetComponent<Health>());
        }
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
