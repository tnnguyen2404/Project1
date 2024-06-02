using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using PathBerserker2d;

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
    public Transform target;
    public NavAgent agent;
    public int facingDirection = -1;
    public float stateTime;
    public float timeOnLink;
    public float timeToCompleteLink;
    public Vector2 direction;
    public int state = 0;
    private Transform elevatorTrans;
    public float deltaDistance;
    public bool handleLinkMovement;
    public int minNumberOfLinkExecutions;
    public Vector2 storedLinkStart;

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
    public bool reachedEndofPath = false;
    public bool isJumping;

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
        agent = GetComponent<NavAgent>();
    }

    void Update() {
        currentState.LogicUpdate();
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
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x - 0.3f, wallCheck.position.y));
        Gizmos.color = Color.white;
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - 0.14f));
    }
}
