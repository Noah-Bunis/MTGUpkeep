using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] Transform targetDestination;
    [SerializeField] TMP_Text damageText;
    [SerializeField] GameObject expDrop;
    private bool isAttacking = false;
    private bool isTakingDamage = false;   
    private bool isDying = false;

    Rigidbody2D rigidbody;
    SimpleFlash flash;
    SpriteRenderer sprite;

    [Header ("ENEMY ATTRIBUTES")]
    [SerializeField] public float speed;
    [SerializeField] public int health;
    [SerializeField] public int healthMax;
    [SerializeField] public float damage;
    [SerializeField] public float damageRate;
    [SerializeField] public int maxEnemyGroup;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        flash = GetComponent<SimpleFlash>();
        sprite = GetComponent<SpriteRenderer>();
        targetDestination = GameObject.FindWithTag("Player").transform;
        damageText.text = "";
        speed = Random.Range(speed * 0.9f, speed * 1.25f);
    }

    void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rigidbody.velocity = direction * speed;

        if (direction.x < 0) sprite.flipX = true;
        else sprite.flipX = false;

        if (health <= 0 && !isDying)
        {
            isDying = true;
            flash.Flash();
            Destroy(gameObject, flash.duration);
            GameObject exp = Instantiate(expDrop);
            exp.transform.position = transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (!isAttacking) StartCoroutine(Attack(collision.gameObject.GetComponent<PlayerController>()));
        }

        else if (collision.gameObject.GetComponent<AttackController>())
        {
            if (!isTakingDamage) StartCoroutine(DamageOverTime(collision.gameObject.GetComponent<AttackController>()));
        }
    }

    public IEnumerator ShowDamage(float damage)
    {
        flash.Flash();
        if (damage != 0)
        {
            damageText.text = damage.ToString();
            yield return new WaitForSeconds(0.2f);
            damageText.text = "";
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

    private IEnumerator DamageOverTime(AttackController attack)
    {
        if (!isTakingDamage)
        {
            isTakingDamage = true;
            attack.ApplyDamage(this);
        }
        yield return new WaitForSeconds(1 / attack.weapon.attackRate);
        if (isTakingDamage) isTakingDamage = false;
    }
}
