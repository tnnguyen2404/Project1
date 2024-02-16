using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatsSO")]
public class StatsSO : ScriptableObject
{
    [Header("Patrol State")]
    public float groundCheckDistance;
    public float wallCheckDistance;
    public float moveSpeed;

    [Header("Attack State")]
    public int attackDamage;
    public float attackRange;
    public float attackRadius;
    public float[] attackDetails = new float[2];

    [Header("Get Hit State")]
    public float health;
    
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
}
