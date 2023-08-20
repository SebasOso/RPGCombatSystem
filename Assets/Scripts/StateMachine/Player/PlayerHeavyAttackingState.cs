using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyAttackState : PlayerBaseState
{
    private float previusFrameTime;
    private bool alreadyAppliedForce;
    private HeavyAttacks heavyAttack;
    public PlayerHeavyAttackState(PlayerStateMachine stateMachine, int attackkIndex) : base(stateMachine)
    {
        heavyAttack = stateMachine.HeavyAttacks[attackkIndex];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(heavyAttack.AnimationName, heavyAttack.TransitionDuration);
        stateMachine.InputReader.HeavyAttackEvent += HandleDown;
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime();
        previusFrameTime = normalizedTime;
    }

    public override void Exit()
    {
        stateMachine.InputReader.AttackEvent -= HandleDown;
    }
    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);
        if(stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("HeavyAttack"))
        {
            return nextInfo.normalizedTime;
        }
        else if(!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("HeavyAttack"))
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
        if(heavyAttack.ComboStateIndex == -1){return;}

        if(normalizedTime < heavyAttack.ComboHeavyAttackTime){return;}

        stateMachine.SwitchState
        (
            new PlayerAttackingState
            (
                stateMachine,
                heavyAttack.ComboStateIndex
            )
        );
    }
    private void TryApplyForce()
    {
        if(alreadyAppliedForce){return;}
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * heavyAttack.Force);
        alreadyAppliedForce = true;
    }
    void HandleDown()
    {
        float normalizedTime = GetNormalizedTime();
        TryComboAttack(normalizedTime);
    }
}
