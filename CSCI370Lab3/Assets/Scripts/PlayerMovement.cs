using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D body;
    public float horizontal;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private AudioSource land_noise;
    [SerializeField] private AudioSource jump_noise;

    private bool jumping = false;
    private bool secondJump = false;

    public float runSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        land_noise = GetComponent<AudioSource>();
        jump_noise = GetComponent<AudioSource>();
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
                StartCoroutine("JumpSound");
                jumping = true;
            } else {
                body.AddForce(new Vector2(0, 200));
                StartCoroutine("JumpSound");
                secondJump = true;
            }
        }
    }

    void FixedUpdate() {

        body.velocity = new Vector2(horizontal * runSpeed, body.velocity.y);

    }

    void OnCollisionEnter2D(Collision2D collision2D) {
        if (collision2D.gameObject.tag == "Platform") {
            jumping = false;
            secondJump = false;
            StartCoroutine("LandSound");
        }
    }

    IEnumerator LandSound()
    {
        land_noise.Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        
    }

    IEnumerator JumpSound()
    {
        jump_noise.Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        
    }
}