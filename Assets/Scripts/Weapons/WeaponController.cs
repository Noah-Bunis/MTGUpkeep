using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    float timer;
    int level = 1;
    [SerializeField] public float attackRate;
    [SerializeField] float attackLength;
    [SerializeField] float attackVelocity;
    [SerializeField] public float baseDamage;
    [SerializeField] GameObject attackSprite;
    [SerializeField] public int maxLevel;
    [SerializeField] public bool followPlayer;
    [SerializeField] public float forwardOffset;

    [SerializeField] string[] levelUpgrades;
    Transform player;

    private InputAction aimAction; // Reference to the Input Action for aiming

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        // Initialize the Input Action for aiming with the right stick
        aimAction = new InputAction(binding: "<Gamepad>/rightStick"); // Replace with your desired binding
        aimAction.Enable();

        PointToMouse();
    }

    public void LevelUp()
    {
        if (level < maxLevel)
        {
            level++;
            for (int i = 0; i < levelUpgrades.Length; i++)
            {
                switch (levelUpgrades[i])
                {
                    case "":
                        break;
                    case "rate":
                        attackRate *= 1.1f;
                        break;
                    case "length":
                        attackLength *= 1.1f;
                        break;
                    case "velocity":
                        attackVelocity *= 1.1f;
                        break;
                    case "damage":
                        baseDamage *= 1.2f;
                        break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 aimInput = aimAction.ReadValue<Vector2>();

        if (aimInput != Vector2.zero)
        {
            // Use controller input
            Vector3 aimDirection = new Vector3(aimInput.x, aimInput.y, 0);
            transform.up = aimDirection;
        }
        else
        {
            // Use mouse input
            PointToMouse();
        }

        timer -= Time.deltaTime;
        if (timer < 0f) Attack();
        else if (timer < attackLength) attackSprite.SetActive(false);
    }

    private void Attack()
    {
        GameObject projectile = Instantiate(attackSprite, transform.position + (transform.up * forwardOffset), transform.rotation);
        projectile.SetActive(true);
        projectile.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector3.up * attackVelocity);
        projectile.GetComponent<AttackController>().Decay(attackLength);
        timer = 1 / attackRate;
    }

    public void PointToMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.up = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
    }
}
