using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PLAYER ATTRIBUTES")]
    [SerializeField] public float health;
    [SerializeField] public float healthMax;
    [SerializeField] public float level;
    [SerializeField] public float expTotal;
    [SerializeField] public float expGain;
    [SerializeField] public float crit;
    [SerializeField] public float pickupRange;
    [SerializeField] public float damage;

    [Header("OBJECT REFRENCES")]
    [SerializeField] PlayerMovement movement;

    void FixedUpdate()
    {
        if (health <= 0)
        {
            Debug.Log("You Died!");
        }
        else if (health > healthMax)
        {
            health = healthMax;
        }
    }
}
