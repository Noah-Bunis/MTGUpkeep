using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PLAYER ATTRIBUTES")]
    [SerializeField] public int health;
    [SerializeField] public int healthMax;
    [SerializeField] public int level;
    [SerializeField] public float expTotal;
    [SerializeField] public float expGain;
    [SerializeField] public float crit;
    [SerializeField] public float pickupRange;
    [SerializeField] public float damage;

    [Header("OBJECT REFRENCES")]
    [SerializeField] PlayerMovement movement;
    [SerializeField] GameObject healthBar;

    private bool healthUpdate = false;
    public float timer = 2;

    void FixedUpdate()
    {
        if (healthBar.activeSelf) healthBar.GetComponent<HealthBar>().SetState(health, healthMax);

        if (health <= 0)
        {
            Debug.Log("You Died!");
        }
        else if (health > healthMax)
        {
            health = healthMax;
        }

        timer -= Time.deltaTime;
        if (!healthBar.activeSelf && healthUpdate) 
        {
            timer = 2;
            healthBar.SetActive(true);
        }
        if (timer < 0) 
        {
            healthUpdate = false;
            healthBar.SetActive(false);
        }
    }

    public void HealthUpdate()
    {
        healthUpdate = true;
    }
}
