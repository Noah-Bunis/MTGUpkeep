using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelManager: MonoBehaviour {
        public int level = 1;
        public int queuedLevels = 0;
        public int exp;
        public int gold;
        public GameObject expBar;
        public Text levelText;
        [SerializeField] public GameObject upgradeMenu;
        [SerializeField] public GameObject equippedItems;
        [SerializeField] public PlayerController player;

        public void Awake() {
                upgradeMenu = GameObject.Find("UpgradeMenu");
                upgradeMenu.SetActive(false);
                expBar = GameObject.FindWithTag("ExpBar");
                levelText = GameObject.Find("LevelText").GetComponent < Text > ();
        }

        public void Update() {
                expBar.GetComponent < Slider > ().value = exp;
                expBar.GetComponent < Slider > ().maxValue = TO_LEVEL_UP;
                levelText.text = "Level " + level;
        }

        public int TO_LEVEL_UP {
                get {
                        return level * 20;
                }
        }

        public void AddEXP(int amount) {
                exp += amount;
                CheckCardEffects(amount);
                CheckLevelUp();
        }

        public void AddLevels(int amount)
        {
                queuedLevels += amount;
                level += amount;
        }

        public void AddGold(int amount) {
                gold += amount;
        }

        public void CheckLevelUp() {
                if (exp >= TO_LEVEL_UP) {
                        exp -= TO_LEVEL_UP;
                        level += 1;
                        LevelUp();
                }
                else if (queuedLevels > 0)
                {
                        queuedLevels -= 1;
                        LevelUp();
                }
        }

        public void CheckCardEffects(int amount)
        {
                for (int i = 0; i < equippedItems.transform.childCount; i++)
                {
                        DarkProphecy darkProphecy = equippedItems.transform.GetChild(i).GetComponent<DarkProphecy>();
                        try 
                        {
                                darkProphecy.Trigger(amount);
                        }
                        catch (NullReferenceException) {}
                }
        }

        private void LevelUp() {
                Time.timeScale = 0;
                upgradeMenu.SetActive(true);
                upgradeMenu.GetComponent<UpgradeManager>().DealCards();
        }
}