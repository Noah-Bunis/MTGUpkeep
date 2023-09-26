using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController: MonoBehaviour {
        [Header("PLAYER ATTRIBUTES")]
        [SerializeField] public int health;
        [SerializeField] public int healthMax;
        [SerializeField] public float critRate;
        [SerializeField] public float critDamageMultiplier;
        [SerializeField] public float pickupRange;
        [SerializeField] public float damage;
        [SerializeField] public float speed;
        public float distanceTraveled;

        [Header("OBJECT REFRENCES")]
        [SerializeField] PlayerMovement movement;
        [SerializeField] GameObject healthBar;

        private bool healthUpdate = false;
        public float timer = 2;
        private Vector3 oldPos;

        void Awake() {
                Application.targetFrameRate = Screen.currentResolution.refreshRate;
                oldPos = transform.position;
        }
        void FixedUpdate() {
                if (healthBar.activeSelf) healthBar.GetComponent < HealthBar > ().SetState(health, healthMax);

                if (health <= 0) {
                        SceneManager.LoadSceneAsync(0);
                } else if (health > healthMax) {
                        health = healthMax;
                }

                timer -= Time.deltaTime;
                if (!healthBar.activeSelf && healthUpdate) {
                        timer = 2;
                        healthBar.SetActive(true);
                }
                if (timer < 0) {
                        healthUpdate = false;
                        healthBar.SetActive(false);
                }

                TrackDistance();
        }

        public void HealthUpdate() {
                healthUpdate = true;
        }

        public void TrackDistance() {
                Vector3 distanceVector = transform.position - oldPos;
                float distanceThisFrame = distanceVector.magnitude;
                distanceTraveled += distanceThisFrame;
                oldPos = transform.position;
        }
}