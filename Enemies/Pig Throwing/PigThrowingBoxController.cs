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
    public Transform player;
    public int facingDirection = -1;

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
        playerDetected = Physics2D.Raycast(wallCheck.position, isFacingRight ? Vector2.right : Vector2.left, stats.playerDetectDistance, whatIsPlayer);
        return playerDetected;
    }

    public bool CheckForGround() {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, stats.groundCheckDistance, whatIsGround);
        return isGrounded;
    }

    public bool CheckForAttackRange() {  
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < stats.playerDetectDistance) {
            playerInAttackRange = true;
        } else {
            playerInAttackRange = false;
        }
        return playerInAttackRange;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stats.playerDetectDistance);
    }
}
