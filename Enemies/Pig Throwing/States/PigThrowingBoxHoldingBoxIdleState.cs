using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigThrowingBoxHoldingBoxIdleState : PigThrowingBoxBaseState
{
    public PigThrowingBoxHoldingBoxIdleState(PigThrowingBoxController pigThrowing, string animName) : base (pigThrowing, animName) 
    {

    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (pigThrowing.CheckForAttackRange()) {
            pigThrowing.SwitchState(pigThrowing.attackState);
        } else if (!pigThrowing.CheckForAttackRange() && pigThrowing.CheckForPlayer()) {
            pigThrowing.SwitchState(pigThrowing.chargeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
