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


    public void Decay(float time)
    {
        Destroy(gameObject, time);
    }

    private bool isCrit()
    {
        if (Random.Range(0, 100) <= player.critRate) return true;
        else return false;
    }

    public void ApplyDamage(EnemyContainer enemy)
    {
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
