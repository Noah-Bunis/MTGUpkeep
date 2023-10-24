using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnCollisionEnter2D(Collision2D collision) {
                if (collision.gameObject.GetComponent < PlayerController > ()) 
                {
                        PlayerController player = collision.gameObject.GetComponent < PlayerController > ();
                        player.health -= (int) damage;
                        player.HealthUpdate(damage);
                        Destroy(gameObject);
                }
    }
}
