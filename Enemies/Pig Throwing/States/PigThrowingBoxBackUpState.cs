using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigThrowingBoxBackUpState : PigThrowingBoxBaseState
{
    public PigThrowingBoxBackUpState(PigThrowingBoxController pigThrowing, string animName) : base (pigThrowing, animName) 
    {

    }

    public override void Enter()
    {
        base.Enter();
        pigThrowing.rb.velocity = new Vector2(pigThrowing.stats.dodgeAngle.x * -pigThrowing.facingDirection, pigThrowing.stats.dodgeAngle.y) * pigThrowing.stats.dodgeForce;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!pigThrowing.boxHasBeenPickedUp) {
            pigThrowing.SwitchState(pigThrowing.playerDetectedState);
        } else {
            pigThrowing.SwitchState(pigThrowing.chargeState);
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
