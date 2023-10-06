using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using RPG.Combat;
using RPG.Saving;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField]
    public Rig ArmRig {get; private set;}
    [field: SerializeField]
    public Transform IkTargetTransform {get; private set;}
    [field: SerializeField] 
    public float JumpForce {get; private set;}
    [field: SerializeField]
    public ForceReceiver ForceReceiver {get; private set;}

    [field: SerializeField]
    public CharacterController CharacterController {get; private set;}
    [field: SerializeField]
    public InputReader InputReader {get; private set;}

    [field: SerializeField]
    public float FreeLookMovementSpeed {get;  set;}

    [field: SerializeField]
    public float RunningMovementSpeed {get; private set;}
    [field: SerializeField]
    public float WalkingMovementSpeed {get; private set;}

    [field: SerializeField]
    public Animator Animator {get; private set;}

    [field: SerializeField]
    public float RotationDamping {get; private set;}
    public Transform MainCameraTransform {get; private set;}
    public float PreviousDodgeTime {get; private set;} = Mathf.NegativeInfinity;

    private void Awake() 
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));    
    }
}
