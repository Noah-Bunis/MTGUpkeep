using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardButton : MonoBehaviour
{
    UpgradeManager upgrade;
    [SerializeField] string cardName;

    void Awake()
    {
        upgrade = GameObject.Find("UpgradeMenu").GetComponent<UpgradeManager>();
    }

    public void SelectCard()
    {
        upgrade.SelectCard(cardName);
    }
}
