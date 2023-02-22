using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D body;
    public float horizontal;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool jumping = false;
    private bool secondJump = false;

    public float runSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("horizontal", horizontal);
        if (horizontal < 0) {
            spriteRenderer.flipX = true;
        } else if (horizontal > 0) {
            spriteRenderer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && (!jumping || !secondJump)) {
            print("Space!");
            if (!jumping) {
                body.AddForce(new Vector2(0, 400));
                jumping = true;
            } else {
                body.AddForce(new Vector2(0, 200));
                secondJump = true;
            }
        }
    }

    void FixedUpdate() {

        body.velocity = new Vector2(horizontal * runSpeed, body.velocity.y);

    }

    void OnCollisionEnter2D(Collision2D collision2D) {
        jumping = false;
        secondJump = false;
    }
}