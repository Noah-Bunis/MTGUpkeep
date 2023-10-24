using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZuranOrb : MonoBehaviour
{
        public GameObject player;
        [SerializeField] int distanceThreshold;
        private PlayerController playerStats;

        void Awake()
        {
                player = transform.parent.parent.gameObject;
                playerStats = player.GetComponent<PlayerController>();
        }
        void FixedUpdate()
        {
                if ((int)playerStats.distanceTraveled >= distanceThreshold)
                {
                        playerStats.health += (int)(playerStats.health * 0.1f);
                        if (playerStats.health < playerStats.healthMax) playerStats.HealthUpdate(0);
                        playerStats.distanceTraveled = 0;
                }
        }
}
