using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatsSO")]
public class PigStatsSO : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject deathParticle;

    [Header("Item Drop Variable")]
    public GameObject[] itemDrops;
    public float dropForce;
    public float torque;

    [Header("General Stats")]
    public float maxHealth;

    [Header("Patrol State")]
    public float groundCheckDistance;
    public float wallCheckDistance;
    public float moveSpeed;

    [Header("Attack State")]
    public int attackDamage;
    public float attackRange;
    public float attackRadius;
    public float[] attackDetails = new float[2];
    
    [Header("Jump State")]
    public float jumpForce;

    [Header("Charge State")]
    public float chargeSpeed;
    public float chargeTime;
    public float chargeDuration;

    [Header("Player Detection State")]
    public float playerDetectDistance;
    public float playerDetectedWaitTime;
    public float detectionPauseTime;  

    [Header("GetHit State")]
    public float knockBackSpeedX, knockBackSpeedY;
}
