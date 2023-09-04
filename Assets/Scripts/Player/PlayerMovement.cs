using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody;
    [SerializeField] SpriteRenderer sprite;
    Animator animator;
    Vector3 movementVector = new Vector3();
    public float speed = 1.3f;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        rigidbody.velocity = movementVector.normalized * speed;
        
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        if (movementVector.x > 0)
        {
            sprite.flipX = false;
        }
        else if (movementVector.x < 0) 
        {
            sprite.flipX = true;
        }
        
        if (Mathf.Abs(movementVector.x) > 0  || Mathf.Abs(movementVector.y) > 0) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);
    }
}
