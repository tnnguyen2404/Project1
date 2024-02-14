using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigDetectPlayerState : PigBaseState
{
    public PigDetectPlayerState(PigController pig, string animName) : base (pig, animName)
    {
       
    }

    public override void Enter()
    {
        base.Enter();
        pig.alert.SetActive(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        pig.rb.velocity = new Vector2(0f, pig.rb.velocity.y);
        if (Time.time >= pig.stateTime + pig.stats.playerDetectedWaitTime) {
            pig.SwitchState(pig.chargeState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        pig.alert.SetActive(false);
        base.Exit();
    }
}
