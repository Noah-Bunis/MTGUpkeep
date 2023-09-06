using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner: MonoBehaviour {
  [SerializeField] GameObject enemy;
  [SerializeField] Vector2 spawnArea;
  [SerializeField] float spawnTimerMax;
  float maxEnemyGroup = 4;
  float spawnTimerMin = 0.5f;
  float spawnTimer;
  float gameTimer = 60;

  private void FixedUpdate() {
    maxEnemyGroup = enemy.GetComponent<EnemyContainer>().maxEnemyGroup;
    spawnTimer -= Time.deltaTime;
    gameTimer -= Time.deltaTime;
    if (spawnTimer <= 0f) {
      SpawnEnemies();
      spawnTimer = spawnTimerMax;
    }
    if (gameTimer <= 0f) {
      if (spawnTimerMax > spawnTimerMin) spawnTimerMax -= 0.125f;
      gameTimer = 60;
    }
  }

  private void SpawnEnemy() {
    Vector3 position = new Vector3(Random.Range(-spawnArea.x + transform.position.x, spawnArea.x + transform.position.x),
      Random.Range(-spawnArea.y + transform.position.y, spawnArea.y + transform.position.y), 0f);

    GameObject newEnemy = Instantiate(enemy);
    newEnemy.transform.position = position;
  }

  private void SpawnEnemies() {
    for (int i = 0; i < Random.Range(1, maxEnemyGroup); i++) {
      SpawnEnemy();
    }
  }
}