using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
  float timer;
  [SerializeField] bool isProjectile;
  [SerializeField] float attackRate;
  [SerializeField] float attackLength;
  [SerializeField] GameObject attackSprite;
  Transform player;

  void Awake()
  {
    player = GameObject.FindWithTag("Player").transform;
    transform.position = player.position;
  }

  private void FixedUpdate()
  {

    Vector3 mousePosition = Input.mousePosition;
    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    transform.up = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

    transform.position = player.position;



    timer -= Time.deltaTime;
    if (timer < 0f) Attack();
    else if(timer < attackLength) attackSprite.SetActive(false);
  }

    private void Attack()
    {
      if (isProjectile)
      {
        GameObject projectile = Instantiate(attackSprite, transform.position, transform.rotation);
        projectile.SetActive(true);
        projectile.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector3.up * 3);
        projectile.GetComponent<AttackController>().Decay(attackLength);
      }
      else attackSprite.SetActive(true);

      timer = 1 / attackRate;
    }
}