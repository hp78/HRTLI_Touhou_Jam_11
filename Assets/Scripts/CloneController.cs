using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    public List<Vector2> positions = new List<Vector2>();

    bool isAlive = true;
    BoxCollider2D boxCol;
    Rigidbody2D rb2d;
    public SpriteRenderer bodySprite;

    public float elapsedTime = 0f;
    public int currPosIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();

        elapsedTime = 0f;
        currPosIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;

        elapsedTime += Time.deltaTime;

        if(elapsedTime > 0.1f)
        {
            currPosIndex += 1;
            elapsedTime = 0f;

            if ((currPosIndex + 2) > positions.Count)
            {
                isAlive = false;
                return;
            }
            else
            {
                if(positions[currPosIndex + 1].x - positions[currPosIndex].x < 0)
                {
                    bodySprite.flipX = false;
                }
                else if (positions[currPosIndex + 1].x - positions[currPosIndex].x > 0)
                {
                    bodySprite.flipX = true;
                }
            }
        }

        transform.position = Vector2.Lerp(positions[currPosIndex], positions[currPosIndex + 1], elapsedTime * 10f);
    }

    public void Reset()
    {
        isAlive = true;
        elapsedTime = 0f;
        currPosIndex = 0;

        transform.position = positions[0];
    }

    public void CloneDie()
    {
        isAlive = false;
    }
}
