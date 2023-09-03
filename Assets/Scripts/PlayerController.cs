using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public Animator animator;

    public float moveForce = 9f;
    public float sprintForce = 15f;
    float currFacing = 1;

    public float jumpForce = 16f;
    public float jumpTimeMax = 0.5f;
    public float jumpTimeCountdown = 0f;

    public Transform feetPos;
    public float groundTolerance = 0.1f;

    bool leftGround = false;
    bool grounded;
    bool stoppedJumping;
    public LayerMask groundMask;

    bool leftWall = false;
    bool wallTouched = false;
    float wallJumpDelay = 0.1f;
    float wallJumpCountdown = -1f;

    public List<Vector2> recordedPositions = new List<Vector2>();
    float recordCountdown = 0f;

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
        recordCountdown -= Time.deltaTime;
        if(recordCountdown <= 0)
        {
            recordedPositions.Add(transform.position);
        }
    }

    void UpdateMovement()
    {
        grounded = Physics2D.OverlapCircle(feetPos.position, groundTolerance, groundMask);

        if (grounded)
        {
            if(leftGround)
            {
                leftGround = false;
                animator.SetTrigger("TriggerLand");
            }
            jumpTimeCountdown = jumpTimeMax;
        }
        else
        {
            leftGround = true;
        }

        if(wallJumpCountdown < 0)
        {
            wallTouched = (Physics2D.OverlapCircle(transform.position - new Vector3(0.15f, 0), groundTolerance, groundMask)
                        || Physics2D.OverlapCircle(transform.position + new Vector3(0.15f, 0), groundTolerance, groundMask));
        }

        if (wallTouched)
        {
            if(leftWall)
            {
                leftWall = false;
                animator.SetTrigger("TriggerWall");
            }

            jumpTimeCountdown = jumpTimeMax;
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            wallJumpCountdown -= Time.deltaTime;
            leftWall = true;

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
                animator.SetTrigger("TriggerJump");
                rb2d.velocity = new Vector2(jumpForce * -currFacing, jumpForce);
                wallJumpCountdown = wallJumpDelay;
                stoppedJumping = false;
                wallTouched = false;
            } 
            else if(grounded)
            {
                animator.SetTrigger("TriggerJump");
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
            xMovement -= 1f;
            currFacing = -1f;
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            xMovement += 1f;
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

        if (xMovement > 0f)
            playerSprite.flipX = true;
        else if (xMovement < 0f)
            playerSprite.flipX = false;
    }

    public void PlayerDie()
    {
        GameManager.instance.Respawn();
    }
}
