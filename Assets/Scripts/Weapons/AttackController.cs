using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] public WeaponController weapon;
    private float damage;
    PlayerController player;
    private float timer;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        damage = player.damage * weapon.baseDamage;
        if (weapon.followPlayer) transform.position = (weapon.transform.position + (weapon.transform.up * weapon.forwardOffset));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyContainer>())
            {
                EnemyContainer enemy = collision.gameObject.GetComponent<EnemyContainer>();
                if (isCrit()) 
                {
                    enemy.health -= (int)(damage * player.critDamageMultiplier);
                    enemy.StartCoroutine(enemy.ShowDamage((int)(damage * player.critDamageMultiplier)));
                }
                else 
                {
                    enemy.health -= (int)damage;
                    enemy.StartCoroutine(enemy.ShowDamage((int)damage));
                }
            }
    }

    public void Decay(float time)
    {
        Destroy(gameObject, time);
    }

    private bool isCrit()
    {
        if (Random.Range(0, 100) <= player.critRate) return true;
        else return false;
    }
}
