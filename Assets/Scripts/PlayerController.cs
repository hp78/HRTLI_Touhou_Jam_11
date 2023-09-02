using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Sprite playerSprite;


    public float moveForce = 9f;
    public float sprintForce = 15f;
    float currFacing = 1;

    public float jumpForce = 16f;
    public float jumpTimeMax = 0.5f;
    public float jumpTimeCountdown = 0f;

    public Transform feetPos;
    public float groundTolerance = 0.1f;

    bool grounded;
    bool stoppedJumping;
    public LayerMask groundMask;

    bool wallTouched = false;
    float wallJumpDelay = 0.1f;
    float wallJumpCountdown = -1f;

    public List<Vector2> recordedPositions = new List<Vector2>();


    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jumpTimeCountdown = jumpTimeMax;
        wallJumpCountdown = wallJumpDelay;
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

        if(wallJumpCountdown < 0)
            wallTouched = (Physics2D.OverlapCircle(transform.position - new Vector3(0.25f, 0), groundTolerance, groundMask)
                        || Physics2D.OverlapCircle(transform.position + new Vector3(0.25f, 0), groundTolerance, groundMask));

        if (wallTouched)
        {
            jumpTimeCountdown = jumpTimeMax;

            rb2d.velocity = Vector2.zero;
        }
        else
        {
            wallJumpCountdown -= Time.deltaTime;

            if (Input.GetKey(KeyCode.Space) && !stoppedJumping)
            {
                if (jumpTimeCountdown > 0)
                {
                    //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                    jumpTimeCountdown -= Time.deltaTime;
                }
            }
        }
            

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(wallTouched)
            {
                rb2d.velocity = new Vector2(jumpForce * -currFacing, jumpForce);
                wallJumpCountdown = wallJumpDelay;
                stoppedJumping = false;
                wallTouched = false;
            } 
            else if(grounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                stoppedJumping = false;
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


        float xMovement = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            xMovement = -1f;
            currFacing = -1f;
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            xMovement = 1f;
            currFacing = 1f;
        }

        if(wallJumpCountdown > 0)
        {

        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            rb2d.velocity = new Vector2(xMovement * sprintForce, rb2d.velocity.y);

        }
        else
        {
            rb2d.velocity = new Vector2(xMovement * moveForce, rb2d.velocity.y);
        }
    }
}
