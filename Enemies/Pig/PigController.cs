using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public PigBaseState currentState;
    public PigIdleState idleState;
    public PigChargeState chargeState;
    public PigPatrolState patrolState;
    public PigDetectPlayerState detectPlayerState;
    public Rigidbody2D rb;
    private Animation anim;
    public LayerMask whatIsGround, whatIsPlayer;
    public Transform groundCheck, wallCheck;
    public GameObject alert;
    public float groundCheckRadius, wallCheckDistance, playerDetectDistance;

    [Header("PigData")]
    public int health;
    public int damage;
    public float moveSpeed;
    public float jumpForce;
    public float chargeSpeed;
    public int facingDirection;

    [Header("Boolean")]
    public bool isGrounded;
    public bool isTouchingWall;
    public bool isMoving;
    public bool isJumping;
    public bool isAlive;
    public bool isAttacking;
    public bool isFacingRight;
    public bool playerDetected;

    [Header("Detection State")]
    public float detectionPauseTime;
    public float playerDetectedWaitTime = 1;

    [Header("State")]
    public float stateTime;

    void Awake() 
    {
        patrolState = new PigPatrolState(this, "Patrol");
        idleState = new PigIdleState(this, "Idle");
        detectPlayerState = new PigDetectPlayerState(this, "Detection");
        chargeState = new PigChargeState(this, "Charge");

        currentState = patrolState;
        currentState.Enter();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
    }
    void Update()
    {
        currentState.LogicUpdate();
    }

    void FixedUpdate() {
        currentState.PhysicsUpdate();
    }

    public bool CheckForPlayer() {
        playerDetected = Physics2D.Raycast(wallCheck.position, isFacingRight ? Vector2.right : Vector2.left, playerDetectDistance, whatIsPlayer);
        return playerDetected;
    }

    public bool CheckForWall() {
        isTouchingWall = Physics2D.Raycast(wallCheck.position, isFacingRight ? Vector2.right : Vector2.left, wallCheckDistance, whatIsGround);
        return isTouchingWall;
    }

    public bool CheckForGround() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        return isGrounded;
    }

    IEnumerator DetectedPlayer() {
        Debug.Log("Player detected!");
        yield return new WaitForSeconds(1);
    }

    public void HandleAnimation() {

    }

    #region Other Functions
    public void SwitchState(PigBaseState newState) {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
        stateTime = Time.time;
    }

    void OnDrawGizmos() {
        Gizmos.DrawRay(wallCheck.position, (isFacingRight ? Vector2.right : Vector2.left) * playerDetectDistance);
    }
    #endregion
}
