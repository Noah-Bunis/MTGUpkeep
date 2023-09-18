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
                        player.HealthUpdate();
                        Destroy(gameObject);
                }
                else if (collision.gameObject.GetComponent < EnemyContainer > ())
                {
                        Destroy(gameObject, 0.1f);
                }
    }
}
