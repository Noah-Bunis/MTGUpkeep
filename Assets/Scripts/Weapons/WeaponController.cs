using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
  float timer;
  int level = 1;
  [SerializeField] float attackRate;
  [SerializeField] float attackLength;
  [SerializeField] float attackVelocity;
  [SerializeField] public float baseDamage;
  [SerializeField] GameObject attackSprite;
  [SerializeField] public int maxLevel;
  [SerializeField] public bool followPlayer;
  Transform player;

  void Awake()
  {
    player = GameObject.FindWithTag("Player").transform;
    transform.position = player.position;
  }

  public void LevelUp()
  {
    if (level < maxLevel)
    {
      level++;
      attackRate *= 1.2f;
      attackLength *= 1.2f;
      baseDamage *= 1.2f;
    }
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
        GameObject projectile = Instantiate(attackSprite, transform.position, transform.rotation);
        projectile.SetActive(true);
        projectile.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector3.up * attackVelocity);
        projectile.GetComponent<AttackController>().Decay(attackLength);
        timer = 1 / attackRate;
    }
}