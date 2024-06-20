using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private LayerMask whatIsPlayer;
    private float damage = 2;
    private float []attackDetails = new float [2];
    private bool isExploded = false; 
    [SerializeField] public Vector3 explodeRadius;
    public GameObject[] itemsDrop;
    public PigThrowingBoxController pigThrowing;
    [SerializeField] public float dropForce;
    [SerializeField] public float torque;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void  Update() {
        if (isExploded) {
            anim.SetTrigger("Hit");
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.CompareTag("Player")) {
            isExploded = true;
            DropItems();
            StartCoroutine(DestroyThisGameObjAfterHitAnim(0.15f));
        } 
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PickUpHitBox")) {
            Debug.Log("BoxPickedUp");
            Destroy(this.gameObject);
        }
    }

    void AttackHitBox() {
        Collider2D[] detectedGameObj = Physics2D.OverlapBoxAll(transform.position, explodeRadius, whatIsPlayer);
        attackDetails[0] = damage;
        attackDetails[1] = transform.position.magnitude;

        foreach (Collider2D collider in detectedGameObj) {
            if (collider.gameObject.CompareTag("Player")) {
                collider.transform.SendMessage("TakeDamage", attackDetails);
            }
        }
    }

    void DropItems() {
        foreach (var item in itemsDrop) {
            Instantiate(item, torque, dropForce);
        }
    }

    void Instantiate(GameObject prefab, float torque, float dropForce) {
        Rigidbody2D itemRb = Instantiate(prefab, transform.position, quaternion.identity).GetComponent<Rigidbody2D>();
        Vector2 dropVelocity = new Vector2(Random.Range(0.5f,-0.5f), 1) * dropForce;
        itemRb.AddForce(dropVelocity, ForceMode2D.Impulse);
        itemRb.AddTorque(torque, ForceMode2D.Impulse);
    }

    IEnumerator DestroyThisGameObjAfterHitAnim(float delay) {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, explodeRadius);
    }
}
