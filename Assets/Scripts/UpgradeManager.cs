using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeManager: MonoBehaviour {
        public GameObject player;
        public string cardName;
        [SerializeField] public GameObject levelUpParticles;

        [SerializeField] public List<GameObject> cards;
        [SerializeField] public GameObject[] positions;

        void Awake() {
                player = GameObject.FindWithTag("Player");
        }

        public void DealCards()
        {
                List<GameObject> cardsNotDrawn = new List<GameObject>();
                foreach (GameObject card in cards) cardsNotDrawn.Add(card);
                for (int i = 0; i < positions.Length; i++)
                {
                        GameObject targetCard = cardsNotDrawn[UnityEngine.Random.Range(0, cardsNotDrawn.Count)];
                        cardsNotDrawn.Remove(targetCard);
                        Instantiate(targetCard, positions[i].transform);
                        
                }
        }

        public void SelectCard(string cardName) {
                this.cardName = cardName;
                switch (cardName) 
                {
                        case null:
                                break;
                        case "AngelsMercy":
                                UpgradePlayer("health%", 0.35f);
                                break;
                        case "BlueElementalBlast":
                        case "Incinerate":
                                UpgradeWeapon();
                                break;
                        case "LightningGreaves":
                                UpgradePlayer("speed", 1.2f);
                                break;
                }

                Time.timeScale = 1;
                Instantiate(levelUpParticles, player.transform);
                gameObject.SetActive(false);
                foreach(GameObject position in positions) Destroy(position.transform.GetChild(0).gameObject);
        }

        void UpgradeWeapon() {
                GameObject weapon = GameObject.Find(cardName);
                try
                {
                        weapon.GetComponent < WeaponController > ().LevelUp();
                } 
                catch (NullReferenceException ex)
                {
                        GameObject newWeapon = Instantiate(GetCard(cardName).GetComponent < CardButton > ().weapon, player.transform.Find("EQUIPPEDITEMS").transform);
                        newWeapon.name = cardName;
                }
        }

        void UpgradePlayer(string stat, float amount) {
                PlayerController attributes = player.GetComponent < PlayerController > ();

                switch (stat) 
                {
                        case null:
                                break;
                        case "speed":
                                attributes.speed *= amount;
                                break;
                        case "health%":
                                attributes.health += (int)(attributes.healthMax * amount);
                                attributes.HealthUpdate();
                                break;
                }
        }

        GameObject GetCard(string cardName) {
                for (int i = 0; i < cards.Count; i++) {
                        if (cards[i].name == cardName) {
                                return cards[i];
                        }
                }
                return null;
        }
}