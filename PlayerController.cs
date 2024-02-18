using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public PigController pigEnemy;
    private float inputRaw = 0f;
    private bool isAlive;
    private bool isMoving;
    private bool isJumping;
    private bool isAttacking;
    private bool isFacingRight;
    public float health = 90;
    private int attackDamage = 20;
    private float[] attackDetails = new float[2];
    private int enemyFacingDirection;
    public int facingDirection = 1;
    public float groundCheckRadius;
    public Transform GroundCheck;
    public Transform AttackHitPosBox;
    public LayerMask whatIsGround, whatIsDamageable;
    [SerializeField] bool isGrounded;
    [SerializeField] bool applyKnockBack;
    [SerializeField] private float attackRadius;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float knockBackSpeedX, knockBackSpeedY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckInput();
        HandleAnimation();
    }

    void FixedUpdate() {
        Move(inputRaw);
        CheckSurrounding();
    }

    void CheckInput() {
        inputRaw = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetKey(KeyCode.Space) && isGrounded) {
            Jump();
        }

        if(Input.GetMouseButtonDown(0)) {
            Attack();
            isAttacking = true;
        } else {
            isAttacking = false;
        }
    }

    void HandleAnimation() {
        anim.SetBool("isMoving", isMoving);
        anim.SetFloat("yVelocity", rb.velocity.y);       
        anim.SetBool("isGrounded", isGrounded);
        if (isAttacking) {
            anim.SetTrigger("Attack");
        }
    }

    void CheckSurrounding() {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
        isJumping = !isGrounded;
    }

    void Move(float dir) {
        if (Mathf.Abs(dir) > 0) {
            isMoving = true;
        } else if (dir == 0) {
            isMoving = false;
        }

        if (isFacingRight && dir > 0) {
            FlipSprite();
        } else if (!isFacingRight && dir < 0) {
            FlipSprite();
        }

        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
    }

    void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Attack() {

    }

    void CheckAttackHitBox() {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(AttackHitPosBox.position, attackRadius, whatIsDamageable);
        attackDetails[0] = attackDamage;
        attackDetails[1] = transform.position.x;

        foreach (Collider2D collider in detectedObjects) {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    void TakeDamage(float[] attackDetails) {
        health -= attackDetails[0];
        enemyFacingDirection = pigEnemy.GetFacingDirection();
        if (applyKnockBack && health > 0.0f) {
            KnockBack();
        } else {
            Die();
        }
    }

    void KnockBack() {
        rb.velocity = new Vector2(knockBackSpeedX * -enemyFacingDirection, knockBackSpeedY);
    }

    void Die() {
        Destroy(gameObject);
    }

    void FlipSprite() {
        facingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180f, 0);
    }

    public int GetFacingDirection() {
        return facingDirection;
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(AttackHitPosBox.position, attackRadius);
    }
}
