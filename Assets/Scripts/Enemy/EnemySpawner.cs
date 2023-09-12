using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner: MonoBehaviour {
	[SerializeField] public GameObject[] enemies;
	[SerializeField] Vector2 spawnArea;
	[SerializeField] float spawnTimerMax;
	[SerializeField] LevelPattern pattern;
	float maxEnemyGroup = 4;
	float spawnTimerMin = 0.1f;
	float spawnTimer;
	public float gameTimer = 0;

	private void Awake()
	{
		pattern.SwitchEnemyPattern();
	}

	private void FixedUpdate() {
		spawnTimer -= Time.deltaTime;
		gameTimer -= Time.deltaTime;
		if (spawnTimer <= 0f) {
			SpawnEnemy();
			spawnTimer = spawnTimerMax;
		}
		if (gameTimer <= 0f) {
			if (spawnTimerMax > spawnTimerMin) spawnTimerMax -= 0.125f;
			if (pattern.level < pattern.enemies.Length -0.5f); 
				{
					pattern.level += 0.5f;
					pattern.SwitchEnemyPattern();
				}
			gameTimer = 60;
		}
	}

	private void SpawnEnemy() {
		Vector3 position = new Vector3(Random.Range(-spawnArea.x + transform.position.x, spawnArea.x + transform.position.x),
			Random.Range(-spawnArea.y + transform.position.y, spawnArea.y + transform.position.y), 0f);
		for (int i = 0; i < enemies.Length; i++)
		{
			maxEnemyGroup = enemies[i].GetComponent<EnemyContainer>().maxEnemyGroup;
			for (int j = 0; j < (Random.Range(0, maxEnemyGroup)); j++)
			{
				GameObject newEnemy = Instantiate(enemies[i]);
				newEnemy.transform.position = position;
			}
		}
	}
}