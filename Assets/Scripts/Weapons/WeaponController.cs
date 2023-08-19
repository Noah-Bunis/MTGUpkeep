using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
   public bool isAttacking = false;
   [SerializeField] float attackRate;
   [SerializeField] float attackLength;
   [SerializeField] GameObject attackSprite;

   private void FixedUpdate()
   {
        if (!isAttacking)
        {
            StartCoroutine(Attack());
        }

        transform.up = transform.parent.up;
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

        yield return new WaitForSeconds(attackRate);
        isAttacking = false;
   }
}
