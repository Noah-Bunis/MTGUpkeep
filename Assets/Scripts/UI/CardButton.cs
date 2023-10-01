using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardButton : MonoBehaviour
{
        UpgradeManager upgrade;
        public string cardName;
        [SerializeField] public GameObject item;

        void Awake()
        {
                upgrade = GameObject.Find("UpgradeMenu").GetComponent<UpgradeManager>();
        }

        public void SelectCard()
        {
                upgrade.SelectCard(cardName);
        }
}
