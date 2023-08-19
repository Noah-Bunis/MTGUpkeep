using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] Transform targetDestination;
    private bool isAttacking = false;

    Rigidbody2D rigidbody;

    [Header ("ENEMY ATTRIBUTES")]
    [SerializeField] float speed;
    [SerializeField] float health;
    [SerializeField] float damage;
    [SerializeField] float damageRate;

    void Awake ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rigidbody.velocity = direction * speed;
    }

    private void OnTriggerStay2D(Collider2D collision)
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
            player.health -= damage;
        }
        yield return new WaitForSeconds(damageRate);
        if (isAttacking) isAttacking = false;
    }
}
