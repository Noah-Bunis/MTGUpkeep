using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
   public bool isAttacking = false;
   [SerializeField] float attackRate;
   [SerializeField] float attackLength;
   [SerializeField] GameObject attackSprite;
   Transform player;

   void Awake()
   {
        player = GameObject.FindWithTag("Player").transform;
   }

   private void FixedUpdate()
   {
        if (!isAttacking)
        {
            StartCoroutine(Attack());
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.up = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.position = player.position;
   }

   private IEnumerator Attack()
   {
        if (!isAttacking)
        {
            isAttacking = true;
            attackSprite.SetActive(true);
        }
        yield return new WaitForSeconds(attackLength);
        attackSprite.SetActive(false);

        yield return new WaitForSeconds(1/attackRate);
        isAttacking = false;
   }
}
