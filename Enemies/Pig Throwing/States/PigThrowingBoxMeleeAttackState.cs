using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigThrowingBoxMeleeAttackState : PigThrowingBoxBaseState
{
    public PigThrowingBoxMeleeAttackState(PigThrowingBoxController pigThrowing, string animName) : base (pigThrowing, animName) 
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(pigThrowing.attackHitBoxPos.position, pigThrowing.stats.meleeAttackRadius, pigThrowing.whatIsPlayer);
        pigThrowing.stats.attackDetails[0] = pigThrowing.stats.meleeAttackDamage;
        pigThrowing.stats.attackDetails[1] = pigThrowing.transform.position.x;

        foreach (Collider2D collider in detectedObjects) {
            collider.transform.SendMessage("TakeDamage", pigThrowing.stats.attackDetails);
        }
    }

    public override void AnimaitonFinishedTrigger()
    {
        base.AnimaitonFinishedTrigger();
        pigThrowing.SwitchState(pigThrowing.playerDetectedState);
    }
}
