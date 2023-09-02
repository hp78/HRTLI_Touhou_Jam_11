using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;

    float lifeInterval;

   // Rigidbody2D rigidbody2d;
    Collider2D collider2d;
    // Start is called before the first frame update
    void Start()
    {
        //rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();

        lifeInterval = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lifeInterval -= Time.deltaTime;
        if(lifeInterval < 0.0f)
        {
            collider2d.enabled = false;
            this.gameObject.SetActive(false);

        }
        else
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }


    }
}
