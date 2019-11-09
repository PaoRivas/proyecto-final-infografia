using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator anim;
    private bool derecha = true;
    private bool grounded = true;
    private float jumpCount = 0;

    private bool doubleJumped = true;
    private int numofJumps = 0;
    private int maxJumps = 4;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && derecha)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !derecha)
        {
            // ... flip the player.
            Flip();
        }

        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput == 0)
        {
            anim.SetBool("isRanin", false);
        }
        else
        {
            anim.SetBool("isRanin", true);
        }
    }

    void Jump()
    {
        if (jumpCount < 1 && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("salto");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
            //grounded = false;
            jumpCount = jumpCount + 1;
            //OnCollisionStay2D(background);
        }

    }

    void OnCollisionStay2D(Collision2D col)
    {

        if (col.gameObject.tag == "Background")
            jumpCount = 0;
  
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        derecha = !derecha;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}