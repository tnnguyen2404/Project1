using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigPatrolState : PigBaseState
{
    public PigPatrolState(PigController pig, string animName) : base (pig, animName)
    {
       
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (pig.CheckForWall()) {
            FlipSprite();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (pig.facingDirection == 1) {
            pig.rb.velocity = new Vector2(pig.moveSpeed, pig.rb.velocity.y);
        } else {
            pig.rb.velocity = new Vector2(-pig.moveSpeed, pig.rb.velocity.y);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    void FlipSprite() {
        pig.facingDirection *= -1;
        pig.isFacingRight = !pig.isFacingRight;
        pig.transform.Rotate(0, 180, 0);
    }
}