using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] public Text timeText;

    float timeToDisplay;

    void FixedUpdate()
    {
        DisplayTime();
    }

    void DisplayTime()
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt((timeToDisplay / 50) % 60);
        float minutes = Mathf.FloorToInt((timeToDisplay / 3000) % 60);

        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
