using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int level = 1;
    public int exp;
    public Slider ExpBar;

    public void Awake()
    {
        ExpBar = GameObject.FindWithTag("ExpBar").GetComponent<Slider>();
    }

    public void FixedUpdate()
    {
        ExpBar.value = exp;
        ExpBar.maxValue = TO_LEVEL_UP;
    }
    public int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    public void AddEXP(int amount)
    {
        exp += amount;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (exp >= TO_LEVEL_UP)
        {
            exp -= TO_LEVEL_UP;
            level += 1;
        }
    }
}
