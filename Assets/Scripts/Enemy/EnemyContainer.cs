using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] Transform targetDestination;
    private bool isAttacking = false;

    Rigidbody2D rigidbody;
    SimpleFlash flash;
    SpriteRenderer sprite;

    [Header ("ENEMY ATTRIBUTES")]
    [SerializeField] public float speed;
    [SerializeField] public int health;
    [SerializeField] public int healthMax;
    [SerializeField] public float damage;
    [SerializeField] public float damageRate;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        flash = GetComponent<SimpleFlash>();
        sprite = GetComponent<SpriteRenderer>();
        targetDestination = GameObject.FindWithTag("Player").transform;

        speed = Random.Range(speed * 0.75f, speed * 1.25f);
    }

    void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rigidbody.velocity = direction * speed;

        if (direction.x < 0) sprite.flipX = true;
        else sprite.flipX = false;

        if (health <= 0)
        {
            flash.Flash();
            Destroy(gameObject, flash.duration);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (!isAttacking) StartCoroutine(Attack(collision.gameObject.GetComponent<PlayerController>()));
        }
    }

    private IEnumerator Attack(PlayerController player)
    {
        if (!isAttacking)
        {
            isAttacking = true;
            player.health -= (int)damage;
            player.HealthUpdate();
        }
        yield return new WaitForSeconds(1 / damageRate);
        if (isAttacking) isAttacking = false;
    }
}
