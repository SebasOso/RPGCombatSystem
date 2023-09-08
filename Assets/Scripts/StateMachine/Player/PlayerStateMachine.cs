using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] 
    public Weapon DefaultWeapon {get; private set;}
    [field: SerializeField] 
    public Transform RightHandSocket {get; private set;}
    [field: SerializeField] 
    public Transform LeftHandSocket {get; private set;}
    
    [field: SerializeField]
    public ForceReceiver ForceReceiver {get; private set;}

    [field: SerializeField]
    public CharacterController CharacterController {get; private set;}

    [field: SerializeField]
    public float TargetingMovementSpeed {get; private set;}

    [field: SerializeField]
    public InputReader InputReader {get; private set;}

    [field: SerializeField]
    public float FreeLookMovementSpeed {get;  set;}

    [field: SerializeField]
    public float RunningMovementSpeed {get; private set;}

    [field: SerializeField]
    public Animator Animator {get; private set;}

    [field: SerializeField]
    public float RotationDamping {get; private set;}

    [field: SerializeField]
    public Targeter Targeter {get; private set;}

    [field: SerializeField]
    public Attack[] Attacks {get; private set;}
    public Transform MainCameraTransform {get; private set;}
    public Weapon currentWeapon {get; private set;}

    private void Start() 
    {
        EquipWeapon(DefaultWeapon);
        
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }
    public void EquipWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        weapon.Spawn(RightHandSocket, LeftHandSocket, Animator);
    }
    void Shoot()
    {
        if(Targeter.currentTarget == null){return;}
        if(currentWeapon.HasProjectile())
        {
            currentWeapon.LaunchProjectile(RightHandSocket,LeftHandSocket,Targeter.currentTarget.GetComponent<Health>());
        }
    }
}
