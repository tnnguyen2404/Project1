using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigThrowingBoxGetHitState : PigThrowingBoxBaseState
{
    public PigThrowingBoxGetHitState(PigThrowingBoxController pigThrowing, string animName) : base (pigThrowing, animName) 
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
    }

    public override void AnimaitonFinishedTrigger()
    {
        base.AnimaitonFinishedTrigger();
        pigThrowing.SwitchState(pigThrowing.idleState);
    }
}
