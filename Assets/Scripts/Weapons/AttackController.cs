using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] public float baseDamage;
    [SerializeField] public WeaponController weapon;
    private float damage;
    PlayerController player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        damage = player.damage * baseDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyContainer>())
            {
                EnemyContainer enemy = collision.gameObject.GetComponent<EnemyContainer>();
                enemy.health -= damage;
            }
    }

    public void Decay(float time)
    {
        Destroy(gameObject, time);
    }
}
