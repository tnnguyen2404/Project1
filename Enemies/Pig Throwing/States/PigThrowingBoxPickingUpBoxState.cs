using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigThrowingBoxPickingUpBoxState : PigThrowingBoxBaseState
{
    public PigThrowingBoxPickingUpBoxState(PigThrowingBoxController pigThrowing, string animName) : base (pigThrowing, animName) 
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= pigThrowing.stats.pickingUpBoxWaitTime) {
            if (pigThrowing.CheckForAttackRange()) {
                pigThrowing.SwitchState(pigThrowing.attackState);
            } else {
                pigThrowing.SwitchState(pigThrowing.chargeState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
