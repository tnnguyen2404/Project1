using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigIdleState : PigBaseState
{
    public PigIdleState(PigController pig, string animName) : base (pig, animName)
    {
       
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (pig.CheckForPlayer()) {
            pig.SwitchState(pig.detectPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        pig.rb.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
