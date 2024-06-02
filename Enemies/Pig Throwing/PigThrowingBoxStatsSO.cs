using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PigThrowingBoxStatsSO")]
public class PigThrowingBoxStatsSO : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject deathParticle;

    [Header("Item Drop Variable")]
    public GameObject[] itemDrops;
    public float dropForce;
    public float torque;

    [Header("General Stats")]
    public float maxHealth;
    public float jumpSpeed;
    public float fallSpeed;

    [Header("CheckSurrounding")]
    public float groundCheckDistance;
    public float wallCheckDistance;

    [Header("Attack State")]
    public float attackRange;

    [Header("Charge State")]
    public float chargeSpeed;
    public float chargeTime;
    public float chargeDuration;
    public float closeEnoughRadius;
    public float travelStopRadius;
    public float targetPredictionTime;

    [Header("Player Detection State")]
    public float playerDetectDistance;
    public float playerDetectedWaitTime;
    public float detectionPauseTime;  

    [Header("Picking Up Box State")]
    public float pickingUpBoxWaitTime;

    [Header("Picking Up Box Idle State")]
    public float idleTime;
    public float pickingUpBoxRange;

    [Header("Finding Box State")]
    public float findingBoxDistance;
    public float runSpeed;
    public float jumpRange;
    
    [Header("GetHit State")]
    public float knockBackSpeedX;
    public float knockBackSpeedY;
}
