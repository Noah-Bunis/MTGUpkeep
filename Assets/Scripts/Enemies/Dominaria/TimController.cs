using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimController : MonoBehaviour
{
        [SerializeField] EnemyContainer enemy;
        [SerializeField] GameObject bulletPrefab, firePoint;
        private GameObject player;
        [SerializeField] float distanceToKeep, bulletForce;
        private float attackRate;
        private float timer;

        void Awake()
        {
                if (GameObject.FindObjectsOfType(typeof(TimController)).Length > 2) Destroy(gameObject);
                player = GameObject.FindWithTag("Player");

                attackRate = enemy.damageRate;
                timer = attackRate;
        }

        void FixedUpdate()
        {
                KeepDistance(distanceToKeep);
                timer -= Time.deltaTime;
                if (timer < 0f) Attack();

                firePoint.transform.LookAt(player.transform.position);
        }

        private void KeepDistance(float distance)
        {
                if (Mathf.Abs(Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x)) <= distanceToKeep
                        && Mathf.Abs(Mathf.Abs(player.transform.position.y) - Mathf.Abs(transform.position.y))  <= distanceToKeep)
                {
                        enemy.speed = 0;
                }
                else enemy.speed = 0.6f;
        }

        private void Attack()
        {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = firePoint.transform.position;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.transform.forward * bulletForce, ForceMode2D.Impulse);
                Destroy(bullet, attackRate);
                timer = attackRate;
        }
}
