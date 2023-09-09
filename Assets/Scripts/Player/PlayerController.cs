using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PLAYER ATTRIBUTES")]
    [SerializeField] public int health;
    [SerializeField] public int healthMax;
    [SerializeField] public float critRate;
    [SerializeField] public float critDamageMultiplier;
    [SerializeField] public float pickupRange;
    [SerializeField] public float damage;
    [SerializeField] public float speed;

    [Header("OBJECT REFRENCES")]
    [SerializeField] PlayerMovement movement;
    [SerializeField] GameObject healthBar;

    private bool healthUpdate = false;
    public float timer = 2;

    void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
    void FixedUpdate()
    {
        if (healthBar.activeSelf) healthBar.GetComponent<HealthBar>().SetState(health, healthMax);

        if (health <= 0)
        {
            Application.Quit();
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
