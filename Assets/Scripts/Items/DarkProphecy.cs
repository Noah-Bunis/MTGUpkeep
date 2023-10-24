using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkProphecy : MonoBehaviour
{
        public GameObject player;
        private LevelManager levelManager;
        private PlayerController playerStats;

        void Awake()
        {
                player = transform.parent.parent.gameObject;
                playerStats = player.GetComponent<PlayerController>();
                levelManager = player.GetComponent<LevelManager>();
        }

        public void Trigger(int amount)
        {
                levelManager.exp += (int)(amount * 0.25);
                switch (Random.Range(0,100))
                {
                        case int n when (n <= 15):
                        playerStats.health -= 1;
                        playerStats.HealthUpdate(1);
                        break;
                        case int n when (n > 15):
                        break;
                }
        }
}
