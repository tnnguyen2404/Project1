using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PigThrowingBoxController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public LayerMask whatIsGround, whatIsPlayer, whatIsDamagable;
    public Transform groundCheck, wallCheck;
    public PigThrowingBoxStatsSO stats;
    public PigThrowingBoxBaseState currentState;
    public PigThrowingBoxIdleState idleState;
    public PigThrowingBoxDetectPlayerState playerDetectedState;
    public PigThrowingBoxAttackState attackState;
    public PigThrowingBoxPickingUpBoxState pickingUpBoxState;
    public PigThrowingBoxChargeState chargeState;
    public GameObject alert;
    public Transform player;
    public int facingDirection = -1;
    public float stateTime;

    [Header("Boolean")]
    public bool playerDetected;
    public bool isGrounded;
    public bool isTouchingWall;
    public bool isMoving;
    public bool isAlive;
    public bool isFacingRight;
    public bool playerInAttackRange;
    public bool isAttacking;

    void Awake() {
        idleState = new PigThrowingBoxIdleState(this, "Idle");
        playerDetectedState = new PigThrowingBoxDetectPlayerState(this, "PlayerDetected");
        pickingUpBoxState = new PigThrowingBoxPickingUpBoxState(this, "PickingUpBox");
        chargeState = new PigThrowingBoxChargeState(this, "Charge");
        attackState = new PigThrowingBoxAttackState(this, "Attack");
        currentState = idleState;
        currentState.Enter();
    }
    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
    }
}
