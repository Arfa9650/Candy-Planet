using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange : IntEventInvoker
{
    #region Fields

    int points = 15;

    public bool hasSpawned = false;

    public bool available = true;

    bool firstTime = true;

    bool nepoBaby = false;

    Rigidbody2D rb2d;

    #endregion

    #region Methods

    private void Start()
    {
        unityEvents.Add(EventNames.SpawnApple, new AppleEvent());
        EventManager.AddInvoker(EventNames.SpawnApple, this);
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        if (/*Spawner.isAttached &&*/ transform.parent != null)
        {
            nepoBaby = true;
            rb2d.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            Collider2D[] coll = gameObject.GetComponents<Collider2D>();
            foreach (var i in coll)
            {
                i.enabled = true;
            }
        }
    }

    private void Update()
    {
        if (!Spawner.isAttached && transform.parent != null)
        {
            transform.parent = null;
            rb2d.bodyType = RigidbodyType2D.Dynamic; 
            Collider2D[] coll = gameObject.GetComponents<Collider2D>();
            foreach (var i in coll)
            {
                i.enabled = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (firstTime && nepoBaby)
        {
            //Debug.Log("I did my part");
            Spawner.collided = true;
            firstTime = false;
        }
        if (collision.gameObject.tag == "Orange")
        {
            if (!hasSpawned && collision.gameObject.GetComponent<Orange>().available)
            {
                Vector2 estimate = (transform.position + collision.transform.position) / 2;
                unityEvents[EventNames.SpawnApple].Invoke(estimate, points);
                collision.gameObject.GetComponent<Orange>().hasSpawned = true;
                collision.gameObject.GetComponent<Orange>().available = false;
                available = false;
                hasSpawned = true;
            }
            if (!available)
            {
                Collider2D[] coll = gameObject.GetComponents<Collider2D>();
                foreach (var i in coll)
                {
                    i.enabled = false;
                }

                GetComponent<Animator>().SetTrigger("Exit");

                EventManager.RemoveInvoker(EventNames.SpawnApple, this);
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
