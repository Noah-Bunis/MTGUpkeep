using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeManager: MonoBehaviour {
        public GameObject player;
        public string cardName;
        [SerializeField] public GameObject levelUpParticles;

        [SerializeField] public List<GameObject> cards;
        [SerializeField] public List<GameObject> commonCards;
        [SerializeField] public List<GameObject> uncommonCards;
        [SerializeField] public List<GameObject> rareCards;
        [SerializeField] public GameObject[] positions;

        void Awake() {
                player = GameObject.FindWithTag("Player");
        }

        public void DealCards()
        {
                HashSet<GameObject> cardsNotDrawn = new HashSet<GameObject>();
                for (int i = 0; i < positions.Length; i++)
                {
                        switch (UnityEngine.Random.Range(0, 100))
                        {
                                case int n when (n <= 50):
                                case 0:
                                        cardsNotDrawn.Add(commonCards[UnityEngine.Random.Range(0,commonCards.Count)]);
                                        break;
                                case int n when (n > 50 && n < 90):
                                        cardsNotDrawn.Add(uncommonCards[UnityEngine.Random.Range(0,uncommonCards.Count)]);
                                        break;
                                case int n when (n >= 90):
                                        cardsNotDrawn.Add(rareCards[UnityEngine.Random.Range(0,rareCards.Count)]);
                                        break;
                        }
                }
                List<GameObject> selectedCards = new List<GameObject>(cardsNotDrawn);
                for (int i = 0; i < selectedCards.Count; i++)
                {
                        GameObject targetCard = selectedCards[i];
                        selectedCards.Remove(targetCard);
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
                        case "DarkProphecy":
                                UpgradePlayer("DarkProphecy", 0f);
                                rareCards.Remove(GetCard(cardName));
                                break;
                        case "LightningGreaves":
                                UpgradePlayer("speed", 1.33f);
                                break;
                        case "KrarksThumb":
                                UpgradePlayer("critRate", 20f);
                                break;
                        //Weapon Cards
                        case "BlueElementalBlast":
                        case "Incinerate":
                                UpgradeWeapon();
                                break;
                }

                Time.timeScale = 1;
                Instantiate(levelUpParticles, player.transform);
                gameObject.SetActive(false);
                foreach(GameObject position in positions)
                {
                        try
                        {
                                Destroy(position.transform.GetChild(0).gameObject);
                        }
                        catch(UnityException){}
                }
                player.GetComponent<LevelManager>().CheckLevelUp();
        }

        void UpgradeWeapon() {
                GameObject weapon = GameObject.Find(cardName);
                try
                {
                        weapon.GetComponent < WeaponController > ().LevelUp();
                } 
                catch (NullReferenceException)
                {
                        GameObject newWeapon = Instantiate((GetCard(cardName).GetComponent<CardButton>().weapon), player.transform.Find("EQUIPPEDITEMS").transform);
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
                        case "critRate":
                                attributes.critRate += amount;
                                break;
                        case "DarkProphecy":
                                attributes.hasCardDarkProphecy = true;
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