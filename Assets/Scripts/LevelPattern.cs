using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPattern: MonoBehaviour {
        [SerializeField] public GameObject[] enemies;
        [SerializeField] public EnemySpawner spawner;

        public float level = 0f;

        public void SwitchEnemyPattern() {
                if (level < enemies.Length - 0.5f) level += 0.5f;
                GameObject[] temp = new GameObject[0];
                switch (level) {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                        temp = new GameObject[1];
                        temp[0] = enemies[(int)level];
                        break;
                case 0.5f:
                case 1.5f:
                        temp = enemies[0..(Mathf.CeilToInt(level))];
                        break;
                case 2.5f:
                case 3.5f:
                case 4.5f:
                        temp = enemies[1..(Mathf.CeilToInt(level))];
                        break;
                }
                spawner.enemies = temp;
        }
}