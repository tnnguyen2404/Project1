using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigThrowingBoxBaseState 
{
    protected PigThrowingBoxController pigThrowing;
    protected string animName;
    public PigThrowingBoxBaseState(PigThrowingBoxController pigThrowing, string animName) {
        this.pigThrowing = pigThrowing;
        this.animName = animName;
    }

    public virtual void Enter() {
        pigThrowing.anim.SetBool(animName, true);
    }

    public virtual void LogicUpdate() {

    }

    public virtual void PhysicsUpdate() {

    }

    public virtual void Exit() {
        pigThrowing.anim.SetBool(animName, false);
    }

    public virtual void AnimationAttackTrigger() {

    }

    public virtual void AnimaitonFinishedTrigger() {
        
    }
}
