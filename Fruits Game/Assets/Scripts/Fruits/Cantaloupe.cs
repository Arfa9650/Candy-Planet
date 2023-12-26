using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cantaloupe : IntEventInvoker
{
    #region Fields

    int points = 36;

    public bool hasSpawned = false;

    public bool available = true;

    bool firstTime = true;

    bool nepoBaby = false;

    Rigidbody2D rb2d;

    #endregion

    #region Methods

    private void Start()
    {
        unityEvents.Add(EventNames.SpawnPineapple, new PineappleEvent());
        EventManager.AddInvoker(EventNames.SpawnPineapple, this);
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        if (Spawner.isAttached && transform.parent != null)
        {
            nepoBaby = true;
            rb2d.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    private void Update()
    {
        if (!Spawner.isAttached && transform.parent != null)
        {
            transform.parent = null;
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (firstTime && nepoBaby)
        {
            Spawner.collided = true;
            firstTime = false;
        }
        if (collision.gameObject.tag == "Cantaloupe")
        {
            if (!hasSpawned && collision.gameObject.GetComponent<Cantaloupe>().available)
            {
                Vector2 estimate = (transform.position + collision.transform.position) / 2;
                unityEvents[EventNames.SpawnPineapple].Invoke(estimate, points);
                collision.gameObject.GetComponent<Cantaloupe>().hasSpawned = true;
                collision.gameObject.GetComponent<Cantaloupe>().available = false;
                available = false;
                hasSpawned = true;
            }
            if (!available)
            {
                GetComponent<CircleCollider2D>().enabled = false;

                GetComponent<Animator>().SetTrigger("Exit");

                EventManager.RemoveInvoker(EventNames.SpawnPineapple, this);
                Invoke("KillTarget", 0.1f);
            }
        }
    }

    void KillTarget()
    {
        Destroy(gameObject);
    }
    #endregion
}
