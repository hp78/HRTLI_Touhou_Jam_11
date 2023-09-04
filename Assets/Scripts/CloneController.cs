using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    public List<Vector2> positions = new List<Vector2>();

    [Space(5)]
    bool isAlive = true;
    public BoxCollider2D boxCol;
    public Rigidbody2D rb2d;

    [Space(5)]
    public SpriteRenderer bodySprite;
    public Sprite bodySpriteNorm;
    public Sprite bodySpriteDead;

    [Space(5)]
    public SpriteRenderer spiritSprite;
    public Sprite[] spiritSprites;

    [Space(5)]
    public float elapsedTime = 0f;
    public int currPosIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();

        elapsedTime = 0f;
        currPosIndex = 0;

        int index = Random.Range(0, 4);
        spiritSprite.sprite = spiritSprites[index];
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

        if ((currPosIndex + 2) > positions.Count)
        {
            isAlive = false;
            return;
        }

        transform.position = Vector2.Lerp(positions[currPosIndex], positions[currPosIndex + 1], elapsedTime * 10f);
    }

    public void Reset()
    {
        isAlive = true;
        boxCol.enabled = true;
        bodySprite.enabled = true;
        bodySprite.sprite = bodySpriteNorm;
        spiritSprite.enabled = true;
        elapsedTime = 0f;
        currPosIndex = 0;

        //transform.position = positions[0];

        StartCoroutine(DisableColiliderOnSpawn());
    }

    public void CloneDie(bool isErased)
    {
        isAlive = false;
        bodySprite.sprite = bodySpriteDead;

        if (isErased)
        {
            boxCol.enabled = false;
            bodySprite.enabled = false;
            spiritSprite.enabled = false;
        }
    }

    IEnumerator DisableColiliderOnSpawn()
    {
        yield return new WaitForSeconds(0.01f);
        boxCol.enabled = false;
        yield return new WaitForSeconds(0.25f);
        boxCol.enabled = true;
        yield return null;
    }
}
