using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner: MonoBehaviour {
	[SerializeField] public GameObject[] enemies;
	[SerializeField] Vector2 spawnArea;
	[SerializeField] float spawnTimerMax;
	[SerializeField] LevelPattern pattern;
	public GameObject player;
	float maxEnemyGroup = 4;
	float spawnTimerMin = 0.1f;
	float spawnTimer;
	public float gameTimer = 60;

	private void Awake()
	{
		pattern.SwitchEnemyPattern();
		player = GameObject.FindWithTag("Player");
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
			if (pattern.level < pattern.enemies.Length -1); 
				{
					pattern.SwitchEnemyPattern();
				}
			gameTimer = 60;
		}
	}

	private void SpawnEnemy() {
		Vector3 position = RandomPosition();
		for (int i = 0; i < enemies.Length; i++)
		{
			maxEnemyGroup = enemies[i].GetComponent<EnemyContainer>().maxEnemyGroup;
			for (int j = 0; j < (Random.Range(0, maxEnemyGroup)); j++)
			{
				GameObject newEnemy = Instantiate(enemies[i]);
				newEnemy.transform.position = position + new Vector3(Random.Range(-1,1), Random.Range(-1,1), 0f);
			}
		}
	}

	private Vector3 RandomPosition()
	{	
		bool searching = true;
		Vector3 tempPosition = new Vector3(-spawnArea.x, spawnArea.y, 0f);
		for (int i = 0; i < 100 && searching; i++)
		{
			tempPosition = new Vector3(Random.Range(-spawnArea.x + transform.position.x, spawnArea.x + transform.position.x),
			Random.Range(-spawnArea.y + transform.position.y, spawnArea.y + transform.position.y), 0f);
			if (Mathf.Abs(tempPosition.x) - Mathf.Abs(player.transform.position.x) >= 1
			 && Mathf.Abs(tempPosition.y) - Mathf.Abs(player.transform.position.y) >= 1)
			{
				searching = false;
			}
		}
		return tempPosition;
	}
}