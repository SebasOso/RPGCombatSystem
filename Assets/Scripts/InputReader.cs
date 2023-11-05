using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue {get; private set;}
    public bool  IsAttacking{get; set;}

    public bool IsRunning{get; private set;}

    public bool  IsHeavyAttacking{get; set;}
    public event Action Tornado;
    public bool IsCasting{get; set;}
    private Controls controls;
    private void Start() 
    {
        if(controls == null)
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);

            controls.Player.Enable();
        }
    }
    private void Update() 
    {
            
    }
    private void OnDestroy() 
    {
        controls.Player.Disable();
    }
    public void Disable()
    {
        controls.Player.Disable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLookAround(InputAction.CallbackContext context)
    {
       
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

    public void OnTornado(InputAction.CallbackContext context)
    {
        if(IsCasting){return;}
        if(context.performed)
        {
            Tornado.Invoke();
            IsCasting = true;
        }
    }
}
