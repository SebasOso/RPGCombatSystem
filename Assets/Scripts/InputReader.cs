using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue {get; private set;}
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelTargetEvent;
    public event Action InteractEvent;
    public bool  IsAttacking{get; set;}

    public bool IsRunning{get; private set;}

    public bool  IsHeavyAttacking{get; set;}
    private Controls controls;
    [SerializeField] private GameObject GamePadUI;
    [SerializeField] private GameObject KeyboardUI;
    private void Start() 
    {
        if(controls == null)
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);

            controls.Player.Enable();
        }
    }
    private void OnDestroy() 
    {
        controls.Player.Disable();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed){return;}
        JumpEvent?.Invoke();
    }
    public void OnDodge(InputAction.CallbackContext context)
    {
        if(!context.performed){return;}
        DodgeEvent?.Invoke();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLookAround(InputAction.CallbackContext context)
    {
       
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if(!context.performed){return;}
        TargetEvent?.Invoke();
    }

    public void OnCancelTarget(InputAction.CallbackContext context)
    {
        if(!context.performed){return;}
        CancelTargetEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            IsAttacking = true;
        }
        else
        {
            IsAttacking = false;
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            IsRunning = true;
        }
        else
        {
            IsRunning = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(!context.performed){return;}
        InteractEvent?.Invoke();
    }
}
