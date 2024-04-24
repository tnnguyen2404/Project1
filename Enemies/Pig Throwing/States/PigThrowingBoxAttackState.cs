using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigThrowingBoxAttackState : PigThrowingBoxBaseState
{
    public PigThrowingBoxAttackState(PigThrowingBoxController pigThrowing, string animName) : base (pigThrowing, animName) 
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log(animName);
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
