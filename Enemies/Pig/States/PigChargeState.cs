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
        if (!pig.CheckForPlayer() || pig.stats.chargeTime >= pig.stats.chargeDuration) {
            ReturnToOriginalPos();
        } else {
            if (pig.CheckForAttackRange()) {
                pig.SwitchState(pig.attackState);
            }
            Charge();
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public override void Exit() {
        base.Exit();
    }

    void Charge() {
        pig.rb.velocity = new Vector2(pig.stats.chargeSpeed * -pig.facingDirection, pig.rb.velocity.y);
        pig.stats.chargeTime += Time.deltaTime;
    }

    void ReturnToOriginalPos() {
        pig.transform.position = Vector2.MoveTowards(pig.transform.position, pig.startPos, pig.stats.chargeSpeed * Time.deltaTime);
        if ((Vector2)pig.transform.position == pig.startPos) {
            pig.stats.chargeTime = 0f;
            pig.SwitchState(pig.idleState);
        }
    }
}
