using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPattern: MonoBehaviour {
        [SerializeField] public List<GameObject> enemies;
        [SerializeField] public EnemySpawner spawner;

        public float level = 0f;

        public void SwitchEnemyPattern() {
                if (level < enemies.Count - 0.5f) level += 0.5f;
                List<GameObject> temp = new List<GameObject>();
                switch (level) {
                case 0:
                case 1:
                case 2:
                case 4:
                case 5:
                        temp.Add(enemies[(int)level]);
                        enemies[(int)level] = enemies[(int)level - 1];
                        break;
                case 3:
                        EnhanceEnemies(0.3f);
                        temp.Add(enemies[(int)level]);
                        break;
                        
                case 0.5f:
                case 1.5f:
                        temp = enemies.GetRange(0,Mathf.CeilToInt(level));
                        break;
                case 2.5f:
                case 3.5f:
                case 4.5f:
                case 5.5f:
                        temp = enemies.GetRange(0,Mathf.CeilToInt(level));
                        break;
                }
                spawner.enemies = temp.ToArray();
        }

        public void EnhanceEnemies(float amount)
        {
                foreach (GameObject enemy in enemies)
                {
                        EnemyContainer e = enemy.GetComponent<EnemyContainer>();
                        e.speed += e.speed * amount;
                        e.healthMax += (int)(e.healthMax * amount);
                        e.health += (int)(e.health * amount);
                        e.damage += e.damage * amount;
                        e.damageRate += e.damageRate * amount;
                }
        }
}