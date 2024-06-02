using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigThrowingBoxDetectPlayerState : PigThrowingBoxBaseState
{
    public PigThrowingBoxDetectPlayerState(PigThrowingBoxController pigThrowing, string animName) : base (pigThrowing, animName) 
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(animName);
        pigThrowing.alert.SetActive(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= pigThrowing.stateTime + pigThrowing.stats.playerDetectedWaitTime) {
            pigThrowing.SwitchState(pigThrowing.findingBoxState);
        } 
           
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        pigThrowing.alert.SetActive(false);
    }
}
