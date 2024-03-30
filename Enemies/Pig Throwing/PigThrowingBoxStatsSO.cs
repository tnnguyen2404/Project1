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

    [Header("CheckSurrounding")]
    public float groundCheckDistance;
    public float wallCheckDistance;

    [Header("Attack State")]
    public float attackRange;

    [Header("Player Detection State")]
    public float playerDetectDistance;
    public float playerDetectedWaitTime;
    public float detectionPauseTime;  

    [Header("GetHit State")]
    public float knockBackSpeedX, knockBackSpeedY;
}
