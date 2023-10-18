using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterController : MonoBehaviour
{
        [SerializeField] EnemyContainer enemy;
        [SerializeField] GameObject bulletPrefab, firePoint;
        public GameObject player;
        [SerializeField] float distanceToKeep, bulletForce, attackLength, warningTime, targetTime;
        private float attackRate;
        private float timer;

        void Awake()
        {
                if (GameObject.FindObjectsOfType(typeof(CasterController)).Length > 1) Destroy(gameObject);

                attackRate = enemy.damageRate;
                timer = attackRate;
        }

        void FixedUpdate()
        {
                player = enemy.player;
                KeepDistance(distanceToKeep);
                timer -= Time.deltaTime;
                if (timer < 0f) 
                {
                        enemy.sprite.color = Color.white;
                        Attack();
                }
                else if (timer < warningTime && timer > targetTime) 
                {
                        enemy.sprite.color = Color.cyan;
                        firePoint.transform.LookAt(player.transform.position);
                }
                
        }

        private void KeepDistance(float distance)
        {
                if (Mathf.Abs(Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x)) <= distanceToKeep
                        && Mathf.Abs(Mathf.Abs(player.transform.position.y) - Mathf.Abs(transform.position.y))  <= distanceToKeep)
                {
                        enemy.speed = 0;
                }
                else enemy.speed = enemy.BASESPEED;
        }

        private void Attack()
        {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = firePoint.transform.position;
                bullet.transform.rotation = Quaternion.LookRotation(bullet.transform.forward, player.transform.position - bullet.transform.position);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.transform.forward * bulletForce, ForceMode2D.Impulse);
                Destroy(bullet, (attackLength));
                timer = (1 / attackRate);
        }
}
