using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] public int expYield;
    [SerializeField] int goldYield;
    [SerializeField] BoxCollider2D collider;

    PlayerController player;
    float timer = 10f;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        collider.size *= player.pickupRange;
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<LevelManager>())
        {
            collision.gameObject.GetComponent<LevelManager>().AddEXP(expYield);
            collision.gameObject.GetComponent<LevelManager>().AddGold(goldYield);
            Destroy(gameObject);
        }
    }
}
