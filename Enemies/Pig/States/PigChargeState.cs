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
        pig.stats.chargeTime = 0f;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
            Charge();

            if (pig.CheckForAttackRange()) {
                pig.SwitchState(pig.attackState);
            }

            if (pig.stats.chargeTime >= pig.stats.chargeDuration || !pig.CheckForPlayer()) {
                ReturnToOriginalPos();
                if (Vector2.Distance(pig.transform.position, pig.startPos) < 0.1f) {
                    pig.SwitchState(pig.idleState);
                    FlipSprite();
                }
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

        if (direction.x > 0 && !pig.isFacingRight || direction.x < 0 && pig.isFacingRight) {
            FlipSprite();
        }

        if (Vector2.Distance(pig.transform.position, pig.startPos) < 0.1f) {
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
