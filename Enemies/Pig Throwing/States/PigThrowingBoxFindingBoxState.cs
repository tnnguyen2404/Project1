using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigThrowingBoxFindingBoxState : PigThrowingBoxBaseState
{
    public PigThrowingBoxFindingBoxState(PigThrowingBoxController pigThrowing, string animName) : base (pigThrowing, animName) 
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if (pigThrowing.CheckForBoxNearBy(out pigThrowing.closestBoxPos)) {
            RunTowardsBox();
        }
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

    void FindBox() {

    }

    void RunTowardsBox() {
        Vector3 direction = pigThrowing.closestBoxPos - pigThrowing.transform.position;
        direction.z = 0;
        direction.Normalize();

        float movementDirection = Mathf.Sign(direction.x * pigThrowing.facingDirection);
        pigThrowing.transform.Translate(movementDirection * pigThrowing.stats.runSpeed * Time.deltaTime, 0, 0);

        if (movementDirection > 0 && pigThrowing.facingDirection < 0 || movementDirection < 0 && pigThrowing.facingDirection > 0) {
            Flip();
        }
    }

    void Flip() {
        
    }
}
