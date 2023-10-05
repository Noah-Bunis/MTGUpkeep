using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeper : MonoBehaviour
{
    [SerializeField] public string group,effectType,cardName;
    [SerializeField] public int amount;

    // Start is called before the first frame update
    void Start()
    {
        switch(cardName)
        {
            case null:
                break;
            case "ChainReaction":
                amount *= EnemyCount();
                break;
        }
        Sweep(group, effectType, amount);
        Destroy(gameObject);
    }

    public void Sweep(string group, string effectType, int amount)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(group);
        foreach (GameObject obj in objectsWithTag)
        {
            if (effectType == "damage")
            {
                obj.GetComponent<EnemyContainer>().health -= amount;
                obj.GetComponent<EnemyContainer>().ShowDamage((float)amount);
            }
        }
    }

    private int EnemyCount()
    {
        int enemyCount = 0;
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in objectsWithTag)
        {
            enemyCount++;
        }
        return enemyCount;
    }
}
