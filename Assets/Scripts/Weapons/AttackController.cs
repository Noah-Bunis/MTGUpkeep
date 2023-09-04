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
                if (isCrit()) 
                {
                    enemy.health -= (int)(damage * player.critDamageMultiplier);
                    enemy.StartCoroutine(enemy.ShowDamage((int)(damage * player.critDamageMultiplier)));
                    player.GetComponent<LevelManager>().AddEXP(10);
                }
                else 
                {
                    enemy.health -= (int)damage;
                    enemy.StartCoroutine(enemy.ShowDamage((int)damage));
                    player.GetComponent<LevelManager>().AddEXP(10);
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
