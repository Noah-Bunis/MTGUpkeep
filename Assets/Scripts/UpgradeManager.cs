using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject player;
    public string cardName;
    [SerializeField] public GameObject levelUpParticles;
    
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
            case "BlueElementalBlast":
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
        WeaponController weapon = player.transform.Find("--EQUIPPED ITEMS--").Find(cardName).GetComponent<WeaponController>();
        if (weapon != null)
        {
            weapon.LevelUp();
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
}
