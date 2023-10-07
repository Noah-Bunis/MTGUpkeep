using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeManager: MonoBehaviour {
        public GameObject player;
        public string cardName;
        public Card emptyCard;
        [SerializeField] public GameObject levelUpParticles;
        [SerializeField] public GameObject blackLotusParticles;
        [SerializeField] public CursorManager cursor;

        [SerializeField] public List<Card> cards;
        [SerializeField] public List<Card> commonCards;
        [SerializeField] public List<Card> uncommonCards;
        [SerializeField] public List<Card> rareCards;
        [SerializeField] public List<Card> mythicCards;
        [SerializeField] public GameObject[] positions;

        void Awake() {
                player = GameObject.FindWithTag("Player");
        }

        public void DealCards()
        {
                cursor.UnlockCursor();
                Cursor.visible = true;
                HashSet<Card> cardsNotDrawn = new HashSet<Card>();
                for (int i = 0; i < positions.Length; i++)
                {
                        int rarity = UnityEngine.Random.Range(0, 100);
                        switch (rarity)
                        {
                                case int n when (n <= 50):
                                        cardsNotDrawn.Add(commonCards[UnityEngine.Random.Range(0,commonCards.Count)]);
                                        break;
                                case int n when (n > 50 && n < 85):
                                        cardsNotDrawn.Add(uncommonCards[UnityEngine.Random.Range(0,uncommonCards.Count)]);
                                        break;
                                case int n when (n >= 85 && n < 95):
                                        cardsNotDrawn.Add(rareCards[UnityEngine.Random.Range(0,rareCards.Count)]);
                                        break;
                                case int n when (n >= 95):
                                        cardsNotDrawn.Add(mythicCards[UnityEngine.Random.Range(0,mythicCards.Count)]);
                                        break;
                        }
                }
                List<Card> selectedCards = new List<Card>(cardsNotDrawn);
                for (int i = 0; i < selectedCards.Count; i++)
                {
                        Card targetCard = selectedCards[i];
                        positions[i].GetComponent<CardDisplay>().card = targetCard;
                        positions[i].GetComponent<Button>().interactable = true;
                }
                for (int i = 0; i < positions.Length; i++)
                {
                        if (positions[i].GetComponent<CardDisplay>().card == emptyCard)
                        {
                                positions[i].SetActive(false);
                                positions[i].GetComponent<Button>().interactable = false;
                        }
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
                        case "BlackLotus":
                                player.GetComponent<LevelManager>().AddLevels(3);
                                break;
                        case "ChainReaction":
                                AddItem();
                                break;
                        case "DarkProphecy":
                                AddItem();
                                break;
                        case "FieryEmancipation":
                                UpgradePlayer("critDamage", 1.5f);
                                break;
                        case "KrarksThumb":
                                UpgradePlayer("critRate", 12f);
                                break;
                        case "LightningGreaves":
                                UpgradePlayer("speed", 1.33f);
                                break;
                        case "ZuranOrb":
                                AddItem();
                                break;
                        //Weapon Cards
                        case "BlueElementalBlast":
                        case "Incinerate":
                                UpgradeWeapon();
                                break;
                }
                FinishSelection();
        }

        void UpgradeWeapon() {
                GameObject weapon = GameObject.Find(cardName);
                try
                {
                        weapon.GetComponent < WeaponController > ().LevelUp();
                } 
                catch (NullReferenceException)
                {
                        GameObject newWeapon = Instantiate((GetCard(cardName).item), player.transform.Find("EQUIPPEDITEMS").transform);
                        newWeapon.name = cardName;
                }
        }

        void AddItem() 
        {
                GameObject item = Instantiate((GetCard(cardName).item), player.transform.Find("EQUIPPEDITEMS").transform);
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
                        case "critDamage":
                                attributes.critDamageMultiplier *= amount;
                                break;
                }
        }

        Card GetCard(string cardName) {
                for (int i = 0; i < cards.Count; i++) {
                        if (cards[i].name == cardName) {
                                return cards[i];
                        }
                }
                return null;
        }


        private void FinishSelection()
        {
                Time.timeScale = 1;
                cursor.LockCursor();
                if (cardName == "BlackLotus") Instantiate(blackLotusParticles, player.transform); else Instantiate(levelUpParticles, player.transform);
                Cursor.visible = false;
                gameObject.SetActive(false);
                foreach(GameObject card in positions)
                {
                        try
                        {
                                card.GetComponent<CardDisplay>().card = emptyCard;
                        }
                        catch(UnityException){}
                }
                player.GetComponent<LevelManager>().CheckLevelUp();
        }
}