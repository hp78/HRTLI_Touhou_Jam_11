using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Sprite playerSprite;

    public float moveForce = 9f;
    public float sprintForce = 15f;

    public float jumpForce = 12f;
    public float jumpTimeMax = 1f;
    public float jumpTimeCountdown = 0f;

    public Transform feetPos;
    public float groundTolerance = 0.1f;

    bool grounded;
    bool stoppedJumping;
    public LayerMask groundMask;

    public List<Vector2> recordedPositions = new List<Vector2>();


    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jumpTimeCountdown = jumpTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        RecordMovement();
    }

    void RecordMovement()
    {

    }

    void UpdateMovement()
    {
        grounded = Physics2D.OverlapCircle(feetPos.position, groundTolerance, groundMask);
        if (grounded)
            jumpTimeCountdown = jumpTimeMax;

        float xMovement = 0f;
        //float yMovement = rb2d.velocity.y;

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(grounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                stoppedJumping = false;
            }
        }

        if(Input.GetKey(KeyCode.Space) && !stoppedJumping)
        {
            if(jumpTimeCountdown > 0)
            {
                //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                jumpTimeCountdown -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            if (jumpTimeCountdown > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            }
            jumpTimeCountdown = 0;
            stoppedJumping = true;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            xMovement = -1f;
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            xMovement = 1f;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            rb2d.velocity = new Vector2(xMovement * sprintForce, rb2d.velocity.y);

        }
        else
        {
            rb2d.velocity = new Vector2(xMovement * moveForce, rb2d.velocity.y);
        }
    }
}
