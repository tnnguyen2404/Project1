using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Pathfinding;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;

public class PigThrowingBoxFindingBoxState : PigThrowingBoxBaseState
{
    public PigThrowingBoxFindingBoxState(PigThrowingBoxController pigThrowing, string animName) : base (pigThrowing, animName) 
    {

    }

    public override void Enter()
    {
        base.Enter();
        pigThrowing.InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");

        GameObject closestBox = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject box in boxes) {
            float distance = Vector2.Distance(pigThrowing.transform.position, box.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestBox = box;
            }
        }

        pigThrowing.target = closestBox.transform;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (pigThrowing.CheckForBoxNearBy(out pigThrowing.closestBoxPos)) {
            if (!pigThrowing.CheckForPickUpRange()) {
                RunTowardsBox();
            } else {
                pigThrowing.SwitchState(pigThrowing.pickingUpBoxState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    void RunTowardsBox() {
        Vector2 direction = pigThrowing.closestBoxPos - pigThrowing.transform.position;
        direction.Normalize();

        pigThrowing.rb.velocity = direction * pigThrowing.stats.runSpeed;

        if (direction.x > 0 && !pigThrowing.isFacingRight || direction.x < 0 && pigThrowing.isFacingRight) {
            Flip();
        }
    }

    void Flip() {
        pigThrowing.transform.Rotate(0,180,0);
        pigThrowing.facingDirection *= -1;
        pigThrowing.isFacingRight = !pigThrowing.isFacingRight;
    }

    void RunTowardsTarget() {
        if (pigThrowing.path == null) {
            return;
        }

        if (pigThrowing.currentWaypoint >= pigThrowing.path.vectorPath.Count) {
            pigThrowing.reachedEndofPath = true;
            return;
        } else {
            pigThrowing.reachedEndofPath = false;
        }

        Vector2 direction = ((Vector2)pigThrowing.path.vectorPath[pigThrowing.currentWaypoint] - pigThrowing.rb.position).normalized;
        Vector2 force = direction * pigThrowing.stats.runSpeed * Time.deltaTime;
        pigThrowing.rb.AddForce(force);

        if (direction.x > 0 && !pigThrowing.isFacingRight || direction.x < 0 && pigThrowing.isFacingRight) {
            Flip();
        }

        float distance = Vector2.Distance(pigThrowing.rb.position, pigThrowing.path.vectorPath[pigThrowing.currentWaypoint]);

        if (distance < pigThrowing.nextWaypointDistance) {
            pigThrowing.currentWaypoint++;
        }
    }

    public void UpdatePath() {
        if (pigThrowing.seeker.IsDone()) {
            pigThrowing.seeker.StartPath(pigThrowing.rb.position, pigThrowing.target.position, pigThrowing.OnPathComplete);
        }
    }
}
