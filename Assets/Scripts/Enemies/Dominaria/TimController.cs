using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimController : MonoBehaviour
{
    [SerializeField] EnemyContainer enemy;
    private GameObject player;
    [SerializeField] float distanceToKeep;

    void Awake()
    {
        if (GameObject.FindObjectsOfType(typeof(TimController)).Length > 2) Destroy(gameObject);
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        KeepDistance(distanceToKeep);
    }

    private void KeepDistance(float distance)
    {
        //TODO
    }
}
