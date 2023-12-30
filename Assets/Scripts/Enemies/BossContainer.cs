using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossContainer : MonoBehaviour
{
        [SerializeField] public int objectLimit;
        [SerializeField] public HealthBar healthBar;
        [SerializeField] public EnemyContainer enemy;
        public EnemySpawner spawner;

        void Awake()
        {
                if (GameObject.FindObjectsOfType(typeof(BossContainer)).Length > objectLimit) Destroy(gameObject);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                        if(enemy.GetComponent<BossContainer>() == null)
                        {
                                Destroy(enemy);
                        }
                }
        }

        void FixedUpdate()
        {
                healthBar.SetState(enemy.health, enemy.healthMax);
                if (enemy.health <= 0)
                {
                        spawner.isSpawning = true;
                        spawner.gameTimer = 0;
                }
        }
}
