using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class LiveBarController : MonoBehaviour
{
    public GameObject[] hearts;
    public PlayerController player;
    public float timeToPlayAnimation;
    public float health = 3;
    public float numOfHeart = 3;
    private void Start() {

    }

    private void FixedUpdate() {
        //health = player.GetPlayerCurHealth();
    }

    private void Update() {
        HealthLost();
        HealthGain();
    }

    void HealthLost() {
        
    }

    void HealthGain() {

    }
}
