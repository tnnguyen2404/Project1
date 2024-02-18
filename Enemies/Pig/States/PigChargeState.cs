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
            pig.stats.chargeTime = 0f;
            Charge();

            if (pig.CheckForAttackRange()) {
                pig.SwitchState(pig.attackState);
            }

            if (pig.stats.chargeTime >= pig.stats.chargeDuration || !pig.CheckForPlayer()) {
                pig.SwitchState(pig.idleState);
                ReturnToOriginalPos();
            }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public override void Exit() {
        base.Exit();
    }

    void Charge() {
        pig.rb.velocity = new Vector2(pig.stats.chargeSpeed * pig.facingDirection, pig.rb.velocity.y);
        pig.stats.chargeTime += Time.deltaTime;
    }

    void ReturnToOriginalPos() {
        Vector2 direction = (pig.startPos - (Vector2)pig.transform.position).normalized;
        pig.rb.velocity = direction * pig.stats.chargeSpeed;
        if ((Vector2)pig.transform.position == pig.startPos) {
            pig.stats.chargeTime = 0f;
            pig.rb.velocity = Vector2.zero;
        }
    }

    void FlipSprite() {
        pig.facingDirection *= -1;
        pig.isFacingRight = !pig.isFacingRight;
        pig.transform.Rotate(0, 180, 0);
    }
}
