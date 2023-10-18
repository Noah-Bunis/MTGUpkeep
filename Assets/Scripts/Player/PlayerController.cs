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
        [SerializeField] public bool isClone;
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
                if (isClone) transform.position += new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f,0.5f), 0f);
        }
        void FixedUpdate() {
                if (healthBar.activeSelf) healthBar.GetComponent < HealthBar > ().SetState(health, healthMax);

                if (health <= 0) 
                {
                        
                        if (isClone) Destroy(gameObject);
                        else SceneManager.LoadSceneAsync(0);
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