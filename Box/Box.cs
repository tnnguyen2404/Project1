using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private LayerMask whatIsPlayer;
    private float damage = 2;
    private float []attackDetails = new float [2];
    private bool isExploded = false; 
    [SerializeField] public float explodeRadius;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.CompareTag("Player")) {
            isExploded = true;
        }
    }

    void AttackHitBox() {
        Collider2D[] detectedGameObj = Physics2D.OverlapCircleAll(transform.position, explodeRadius, whatIsPlayer);
        attackDetails[0] = damage;
        attackDetails[1] = transform.position.magnitude;

        foreach (Collider2D collider in detectedGameObj) {
            if (collider.gameObject.CompareTag("Player")) {
                collider.transform.SendMessage("TakeDamage", attackDetails);
            }
        }
    }
}
