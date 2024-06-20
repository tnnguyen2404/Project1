using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float speed;
    private Rigidbody2D rb;

    void Start() {
        rb  = GetComponent<Rigidbody2D>();
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        Vector2 force = direction * speed;
        rb.AddForce(force);
    }

    void Update() {
        
    }

    public void InitializeProjectile(Transform target, float speed) {
        this.target = target;
        this.speed = speed;
    }
}
