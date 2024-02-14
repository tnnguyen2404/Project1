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
        pig.anim.SetBool(animName, true);
    }

    public virtual void LogicUpdate() {

    }

    public virtual void PhysicsUpdate() {

    }

    public virtual void Exit() {
        pig.anim.SetBool(animName, false);
    }

    public virtual void AnimationAttackTrigger() {

    }

    public virtual void AnimaitonFinishedTrigger() {
        
    }
}
