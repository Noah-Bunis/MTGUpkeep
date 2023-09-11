using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject player;
    public string cardName;
    [SerializeField] public GameObject levelUpParticles;

    [SerializeField] public GameObject[] cards;
    
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    public void SelectCard(string cardName)
    {
        this.cardName = cardName;
        switch (cardName)
        {
            case null:
                break;
            case "BlueElementalBlast": case "Incinerate":
                UpgradeWeapon();
                break;
            case "LightningGreaves":
                UpgradePlayer("speed", 1.2f);
                break;
        }

        Time.timeScale = 1;
        Instantiate(levelUpParticles, player.transform);
        gameObject.SetActive(false);
    }

    void UpgradeWeapon()
    {
        GameObject weapon = GameObject.Find(cardName);
        if (weapon.GetComponent<WeaponController>() != null)
        {
            weapon.GetComponent<WeaponController>().LevelUp();
        }
        else
        {
            GameObject newWeapon = Instantiate(GetCard(cardName).GetComponent<CardButton>().weapon, player.transform.Find("EQUIPPEDITEMS").transform);
            newWeapon.name = cardName;
        }
    }

    void UpgradePlayer(string stat, float amount)
    {
        PlayerController attributes = player.GetComponent<PlayerController>();

        switch (stat)
        {
            case null:
                break;
            case "speed":
                attributes.speed *= amount;
                break;
        }
    }

    GameObject GetCard(string cardName)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].name == cardName) 
            {
                return cards[i];
            }
        }
        return null;
    }
}
