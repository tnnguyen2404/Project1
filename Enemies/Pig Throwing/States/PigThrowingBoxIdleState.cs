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
