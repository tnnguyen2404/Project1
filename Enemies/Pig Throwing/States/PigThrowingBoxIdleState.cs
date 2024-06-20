using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigThrowingBoxIdleState : PigThrowingBoxBaseState
{
    public PigThrowingBoxIdleState(PigThrowingBoxController pigThrowing, string animName) : base (pigThrowing, animName) 
    {

    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (pigThrowing.CheckIfShouldDodge()) {
            pigThrowing.SwitchState(pigThrowing.backUpState);
        } else if (pigThrowing.CheckForPlayer()) {
            pigThrowing.SwitchState(pigThrowing.playerDetectedState);
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
