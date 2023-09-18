using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager: MonoBehaviour {
        [SerializeField] public int expYield;
        [SerializeField] int goldYield;
        [SerializeField] new BoxCollider2D collider;

        public PlayerController player;
        float timer = 10f;

        void FixedUpdate() {
                collider.size = new Vector2(player.GetComponent<PlayerController>().pickupRange, player.GetComponent<PlayerController>().pickupRange);
                timer -= Time.deltaTime;
                if (timer <= 0f) Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
                if (collision.gameObject.GetComponent < LevelManager > ()) {
                        collision.gameObject.GetComponent < LevelManager > ().AddEXP(expYield);
                        collision.gameObject.GetComponent < LevelManager > ().AddGold(goldYield);
                        Destroy(gameObject);
                }
        }
}