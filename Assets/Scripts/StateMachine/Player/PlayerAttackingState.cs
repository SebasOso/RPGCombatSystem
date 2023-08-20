using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private bool alreadyAppliedForce;
    private float previusFrameTime;
    private Attack attack;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        stateMachine.InputReader.IsAttacking = false;
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime();
        if(normalizedTime >= previusFrameTime && normalizedTime < 1f)
        {
            if(stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
                TryApplyForce();
            }
        }
        else
        {
            if(stateMachine.Targeter.currentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
        previusFrameTime = normalizedTime;
    }

    public override void Exit()
    {

    }
    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);
        if(stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if(!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0;
        }
    }
    private void TryComboAttack(float normalizedTime)
    {
        if(attack.ComboStateIndex == -1){return;}

        if(normalizedTime < attack.ComboAttackTime){return;}

        stateMachine.SwitchState
        (
            new PlayerAttackingState
            (
                stateMachine,
                attack.ComboStateIndex
            )
        );
    }
    private void TryApplyForce()
    {
        if(alreadyAppliedForce){return;}
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyAppliedForce = true;
    }
}
