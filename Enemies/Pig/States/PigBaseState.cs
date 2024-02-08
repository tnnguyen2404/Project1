using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PigBaseState
{
    protected PigController pig;
    protected string animName;

    public PigBaseState(PigController pig, string animName) 
    {
        this.pig = pig;
        this.animName = animName;
    }

    public virtual void Enter() {

    }

    public virtual void LogicUpdate() {

    }

    public virtual void PhysicsUpdate() {

    }

    public virtual void Exit() {
        
    }
}
