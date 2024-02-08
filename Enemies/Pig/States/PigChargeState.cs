using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigChargeState : PigBaseState
{
    public PigChargeState(PigController pig, string animName) : base (pig, animName) 
    {

    }

    public override void Enter() {
        base.Enter();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        if (!pig.CheckForPlayer()) {
            pig.SwitchState(pig.patrolState);
        } else {
            Charge();
        }
    }

    public override void Exit() {
        base.Exit();
    }

    void Charge() {
        pig.rb.velocity = new Vector2(pig.chargeSpeed * pig.facingDirection, pig.rb.velocity.y);
    }
}
