using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEditor;
using UnityEngine;

public class PigThrowingBoxController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public LayerMask whatIsGround, whatIsPlayer, whatIsDamagable, whatIsObject;
    public Transform groundCheck, wallCheck;
    public PigThrowingBoxStatsSO stats;
    public PigThrowingBoxBaseState currentState;
    public PigThrowingBoxIdleState idleState;
    public PigThrowingBoxDetectPlayerState playerDetectedState;
    public PigThrowingBoxAttackState attackState;
    public PigThrowingBoxPickingUpBoxState pickingUpBoxState;
    public PigThrowingBoxChargeState chargeState;
    public PigThrowingBoxHoldingBoxIdleState holdingBoxIdleState;
    public PigThrowingBoxFindingBoxState findingBoxState;
    public GameObject alert;
    public Transform player;
    public int facingDirection = -1;
    public float stateTime;
    public Vector3 closestBoxPos;
    public Path path;
    public int currentWaypoint = 0;
    public float nextWaypointDistance = 3f;
    public bool reachedEndofPath = false;
    public Transform target;
    public Seeker seeker;

    [Header("Boolean")]
    public bool playerDetected;
    public bool isGrounded;
    public bool isTouchingWall;
    public bool isMoving;
    public bool isAlive;
    public bool isFacingRight = false;
    public bool playerInAttackRange;
    public bool isAttacking;
    public bool boxIsNearBy;
    public bool isInPickUpRange;

    void Awake() {
        idleState = new PigThrowingBoxIdleState(this, "Idle");
        playerDetectedState = new PigThrowingBoxDetectPlayerState(this, "PlayerDetected");
        pickingUpBoxState = new PigThrowingBoxPickingUpBoxState(this, "PickingUpBox");
        chargeState = new PigThrowingBoxChargeState(this, "Charge");
        attackState = new PigThrowingBoxAttackState(this, "Attack");
        holdingBoxIdleState = new PigThrowingBoxHoldingBoxIdleState(this, "HoldingBoxIdle");
        findingBoxState = new PigThrowingBoxFindingBoxState(this, "FindingBox");
        currentState = idleState;
        currentState.Enter();
    }
    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
    }

    void Update() {
        currentState.LogicUpdate();
        CheckForGround();
    }
    void FixedUpdate() {
        currentState.PhysicsUpdate();
    }
    public bool CheckForPlayer() {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < stats.playerDetectDistance) {
            playerDetected = true;
        } else {
            playerDetected = false;
        }
        return playerDetected;
    }

    public bool CheckForGround() {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, stats.groundCheckDistance, whatIsGround);
        return isGrounded;
    }

    public bool CheckForAttackRange() {  
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < stats.attackRange) {
            playerInAttackRange = true;
        } else {
            playerInAttackRange = false;
        }
        return playerInAttackRange;
    }

    public bool CheckForBoxNearBy(out Vector3 boxPos) {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");

        if (boxes.Length == 0) {
            boxPos = Vector3.zero;
            return false;
        }

        GameObject closestBox = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject box in boxes) {
            float distance = Vector2.Distance(transform.position, box.transform.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestBox = box;
            }
        }

        if (closestDistance <= stats.findingBoxDistance) {
            boxPos = closestBox.transform.position;
            return true;
        }

        boxPos = Vector3.zero;
        return false;
    }

    public void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }
    public bool CheckForPickUpRange() {
        isInPickUpRange = Physics2D.Raycast(wallCheck.position, isFacingRight ? Vector2.right : Vector2.left, stats.pickingUpBoxRange, whatIsObject);
        return isInPickUpRange;
    }

    public void SwitchState(PigThrowingBoxBaseState newState) {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
        stateTime = Time.time;
    }

    public int GetFacingDirection() {
        return facingDirection;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stats.playerDetectDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x - 3, wallCheck.position.y));
        Gizmos.color = Color.white;
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - 0.14f));
    }
}
