using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using PathBerserker2d;

public class PigThrowingBoxFindingBoxState : PigThrowingBoxBaseState
{
    public PigThrowingBoxFindingBoxState(PigThrowingBoxController pigThrowing, string animName) : base(pigThrowing, animName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        pigThrowing.CheckForGround();
        pigThrowing.isJumping = !pigThrowing.isGrounded;
        pigThrowing.anim.SetFloat("Jump", pigThrowing.rb.velocity.y);
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");

        GameObject closestBox = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject box in boxes)
        {
            float distance = Vector2.Distance(pigThrowing.transform.position, box.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestBox = box;
            }
        }

        pigThrowing.target = closestBox.transform;

        Vector2 targetPos = pigThrowing.target.position;
        if (closestDistance > pigThrowing.stats.findingBoxDistance &&
            !(pigThrowing.agent.PathGoal.HasValue && Vector2.Distance(pigThrowing.agent.PathGoal.Value, targetPos) < pigThrowing.stats.travelStopRadius))
        {
            if (!pigThrowing.agent.UpdatePath(targetPos) && pigThrowing.stats.targetPredictionTime > 0) {
                pigThrowing.agent.UpdatePath(pigThrowing.target.position);
            }

        } else if (closestDistance <= pigThrowing.stats.pickingUpBoxRange)
        {
            pigThrowing.agent.Stop();
        }

        pigThrowing.agent.OnStartLinkTraversal += Agent_StartLinkTraversalEvent;
        pigThrowing.agent.OnStartSegmentTraversal += Agent_OnSegmentTraversal;

        if (Vector2.Distance(pigThrowing.transform.position, closestBox.transform.position) <= pigThrowing.stats.pickingUpBoxRange)
        {
            pigThrowing.SwitchState(pigThrowing.pickingUpBoxState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        pigThrowing.agent.OnLinkTraversal += Agent_OnLinkTraversal;
        pigThrowing.agent.OnSegmentTraversal += Agent_OnSegmentTraversal;
    }

    public override void Exit()
    {
        base.Exit();
    }

    void Flip()
    {
        pigThrowing.transform.Rotate(0, 180, 0);
        pigThrowing.facingDirection *= -1;
        pigThrowing.isFacingRight = !pigThrowing.isFacingRight;
    }

    void Agent_StartLinkTraversalEvent(NavAgent agent)
    {
        string linkType = agent.CurrentPathSegment.link.LinkTypeName;
        bool unknownLinkType = linkType != "jump" && linkType != "fall";

        pigThrowing.handleLinkMovement = linkType == "jump" || linkType == "fall";

        if (!pigThrowing.handleLinkMovement)
            return;

        pigThrowing.timeOnLink = 0;
        Vector2 delta = agent.PathSubGoal - agent.CurrentPathSegment.LinkStart;
        pigThrowing.deltaDistance = delta.magnitude;
        pigThrowing.direction = delta / pigThrowing.deltaDistance;
        pigThrowing.minNumberOfLinkExecutions = 1;
        pigThrowing.storedLinkStart = agent.CurrentPathSegment.LinkStart;

        if (pigThrowing.direction.x > 0 && !pigThrowing.isFacingRight || pigThrowing.direction.x < 0 && pigThrowing.isFacingRight) {
            Flip();
        }

        float speed = 1;

        switch (agent.CurrentPathSegment.link.LinkTypeName)
        {
            case "fall":
                speed = pigThrowing.stats.fallSpeed;
                break;
            case "jump":
                speed = pigThrowing.stats.jumpSpeed;
                break;
        }

        pigThrowing.timeToCompleteLink = pigThrowing.deltaDistance / speed;
    }

    void Agent_OnLinkTraversal(NavAgent agent)
    {
        if (!pigThrowing.handleLinkMovement)
        {
            return;
        }

        pigThrowing.timeOnLink += Time.deltaTime;
        pigThrowing.timeOnLink = Mathf.Min(pigThrowing.timeToCompleteLink, pigThrowing.timeOnLink);

        switch (agent.CurrentPathSegment.link.LinkTypeName)
        {
            case "jump":
                Jump(agent);
                break;
            case "fall":
                Fall(agent);
                break;
            default:
                Jump(agent);
                break;
        }
        pigThrowing.minNumberOfLinkExecutions--;

        if (pigThrowing.timeOnLink >= pigThrowing.timeToCompleteLink && pigThrowing.minNumberOfLinkExecutions <= 0)
        {
            agent.CompleteLinkTraversal();
            return;
        }
    }

    void Agent_OnStartSegmentTraversal(NavAgent agent)
    {

    }

    void Agent_OnSegmentTraversal(NavAgent agent)
    {
        Vector2 newPos;
        bool reachedGoal = MoveAlongSegment(agent.Position, agent.PathSubGoal, agent.CurrentPathSegment.Point, agent.CurrentPathSegment.Tangent, Time.deltaTime * pigThrowing.stats.runSpeed, out newPos);
        agent.Position = newPos;

        if (reachedGoal)
        {
            agent.CompleteSegmentTraversal();
        }
    }

    private void Jump(NavAgent agent)
    {
        Vector2 newPos = pigThrowing.storedLinkStart + pigThrowing.direction * pigThrowing.timeOnLink * pigThrowing.stats.jumpSpeed;
        newPos.y += pigThrowing.deltaDistance * 0.3f * Mathf.Sin(Mathf.PI * pigThrowing.timeOnLink / pigThrowing.timeToCompleteLink);
        agent.Position = newPos;
    }

    private void Fall(NavAgent agent)
    {
        Vector2 newPos = pigThrowing.storedLinkStart + pigThrowing.direction * pigThrowing.timeOnLink * pigThrowing.stats.fallSpeed;
        agent.Position = newPos;
    }

    private static bool MoveAlongSegment(Vector2 pos, Vector2 goal, Vector2 segPoint, Vector2 segTangent, float amount, out Vector2 newPos)
    {
        pos = Geometry.ProjectPointOnLine(pos, segPoint, segTangent);
        goal = Geometry.ProjectPointOnLine(goal, segPoint, segTangent);
        return MoveTo(pos, goal, amount, out newPos);
    }

    private static bool MoveTo(Vector2 pos, Vector2 goal, float amount, out Vector2 newPos)
    {
        Vector2 dir = goal - pos;
        float distance = dir.magnitude;
        if (distance <= amount)
        {
            newPos = goal;
            return true;
        }

        newPos = pos + dir * amount / distance;
        return false;
    }

}
    
