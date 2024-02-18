using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PigController : MonoBehaviour
{
    public PigBaseState currentState;
    public PigIdleState idleState;
    public PigChargeState chargeState;
    public PigPatrolState patrolState;
    public PigDetectPlayerState detectPlayerState;
    public PigAttackState attackState;
    public PigGetHitState getHitState;
    public Rigidbody2D rb;
    public Animator anim;
    public LayerMask whatIsGround, whatIsPlayer, whatIsDamageable;
    public Transform groundCheck, wallCheck;
    public Transform attackHitBoxPos;
    public Transform player;
    public GameObject alert;
    public StatsSO stats;
    public int facingDirection = 1;
    public Vector2 startPos;
    public Vector2 curPos;
    
    [Header("Boolean")]
    public bool isGrounded;
    public bool isTouchingWall;
    public bool isMoving;
    public bool isJumping;
    public bool isAlive;
    public bool isAttacking;
    public bool isCharging = false;
    public bool isFacingRight;
    public bool playerDetected;
    public bool playerInAttackRange;
    
    [Header("State")]
    public float stateTime;

    void Awake() 
    {
        //patrolState = new PigPatrolState(this, "Patrol");
        idleState = new PigIdleState(this, "Idle");
        detectPlayerState = new PigDetectPlayerState(this, "Detection");
        chargeState = new PigChargeState(this, "Charge");
        attackState = new PigAttackState(this, "Attack");
        getHitState = new PigGetHitState(this, "GetHit");


        currentState = idleState;
        currentState.Enter();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPos = transform.position;
    }
    void Update()
    {
        currentState.LogicUpdate();
    }

    void FixedUpdate() {
        currentState.PhysicsUpdate();
    }
    public bool CheckForPlayer() {
        playerDetected = Physics2D.Raycast(wallCheck.position, isFacingRight ? Vector2.right : Vector2.left, stats.playerDetectDistance, whatIsPlayer);
        return playerDetected;
    }

    public bool CheckForWall() {
        isTouchingWall = Physics2D.Raycast(wallCheck.position, isFacingRight ? Vector2.right : Vector2.left, stats.wallCheckDistance, whatIsGround);
        return isTouchingWall;
    }

    public bool CheckForGround() {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, stats.groundCheckDistance, whatIsGround);
        return isGrounded;
    }

    public bool CheckForAttackRange() {
        playerInAttackRange = Physics2D.Raycast(wallCheck.position, isFacingRight ? Vector2.right : Vector2.left, stats.attackRange, whatIsPlayer);
        return playerInAttackRange;
    }

    #region Other Functions
    public void SwitchState(PigBaseState newState) {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
        stateTime = Time.time;
    }

    public void AnimationAttackTrigger() {
        currentState.AnimationAttackTrigger();
    }

    public void AnimaitonFinishedTrigger() {
        currentState.AnimaitonFinishedTrigger();
    }

    public int GetFacingDirection() {
        return facingDirection;
    }

    void OnDrawGizmos() {
        Gizmos.DrawRay(wallCheck.position, (isFacingRight ? Vector2.right : Vector2.left) * 4);
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - 0.14f));
        Gizmos.DrawWireSphere(attackHitBoxPos.position, stats.attackRadius);
    }
    #endregion
}
