using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyContainer>())
            {
                EnemyContainer enemy = collision.gameObject.GetComponent<EnemyContainer>();
                enemy.health -= damage;
            }
    }
}
