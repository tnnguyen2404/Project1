using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Random = UnityEngine.Random;

public class PigController : MonoBehaviour
{
    public PlayerController playerController;
    public PigBaseState currentState;
    public PigIdleState idleState;
    public PigChargeState chargeState;
    public PigPatrolState patrolState;
    public PigDetectPlayerState detectPlayerState;
    public PigAttackState attackState;
    public PigGetHitState getHitState;
    public PigDeathState deathState;
    public Rigidbody2D rb;
    public Animator anim;
    public LayerMask whatIsGround, whatIsPlayer, whatIsDamageable;
    public Transform groundCheck, wallCheck;
    public Transform attackHitBoxPos;
    public Transform player;
    public GameObject alert;
    public PigStatsSO stats;
    public int facingDirection = 1;
    public Vector2 startPos;
    public Vector2 curPos;
    public float curHealth;
    public int playerFacingDirection;
    
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
    public bool applyKnockBack = true;
    
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
        deathState = new PigDeathState(this, "Death");

        currentState = idleState;
        currentState.Enter();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPos = transform.position;
        curHealth = stats.maxHealth;
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

    private void TakeDamage(float[] attackDetails) {
        curHealth -= attackDetails[0];
        playerFacingDirection = playerController.GetFacingDirection();
        if (curHealth > 0.1f && applyKnockBack) {
            SwitchState(getHitState);
        } else {
            SwitchState(deathState);
        }
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

    public void Instantiate(GameObject prefab, float torque, float dropForce) {
        Rigidbody2D itemRb = Instantiate(prefab, transform.position, quaternion.identity).GetComponent<Rigidbody2D>();
        Vector2 dropVelocity = new Vector2(Random.Range(0.5f,-0.5f), 1) * dropForce;
        itemRb.AddForce(dropVelocity, ForceMode2D.Impulse);
        itemRb.AddTorque(torque, ForceMode2D.Impulse);
    }

    void OnDrawGizmos() {
        Gizmos.DrawRay(wallCheck.position, (isFacingRight ? Vector2.right : Vector2.left) * 4);
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - 0.14f));
        Gizmos.DrawWireSphere(attackHitBoxPos.position, stats.attackRadius);
    }
    #endregion
}
