using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool jumping = false;

    public float jumpForce = 1;
    public float movementForce = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if(!jumping && Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        spriteRenderer.flipX = rb.velocity.x < 0;

        anim.SetBool("bWalking", rb.velocity.x != 0);

        rb.AddForce(Vector2.right * horizontalInput * movementForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        jumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
