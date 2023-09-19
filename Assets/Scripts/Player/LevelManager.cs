using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager: MonoBehaviour {
        public int level = 1;
        public int exp;
        public int gold;
        public GameObject expBar;
        public Text levelText;
        [SerializeField] public GameObject upgradeMenu;

        public void Awake() {
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
                        return level * 100;
                }
        }

        public void AddEXP(int amount) {
                exp += amount;
                CheckLevelUp();
        }

        public void AddGold(int amount) {
                gold += amount;
        }

        private void CheckLevelUp() {
                if (exp >= TO_LEVEL_UP) {
                        exp -= TO_LEVEL_UP;
                        level += 1;
                        LevelUp();
                }
        }

        private void LevelUp() {
                Time.timeScale = 0;
                upgradeMenu.SetActive(true);
                upgradeMenu.GetComponent<UpgradeManager>().DealCards();
        }
}