using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raspberry : IntEventInvoker
{
    #region Fields

    int points = 2;

    public bool hasSpawned = false;

    bool firstTime = true;

    bool nepoBaby = false;

    Rigidbody2D rb2d;

    #endregion

    #region Methods

    private void Start()
    {
        unityEvents.Add(EventNames.SpawnLemon, new LemonEvent());
        EventManager.AddInvoker(EventNames.SpawnLemon, this);
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        if (Spawner.isAttached && transform.parent != null)
        {
            nepoBaby = true;
            rb2d.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            Collider2D[] coll = gameObject.GetComponents<Collider2D>();
            foreach(var i  in coll)
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
            Spawner.collided = true;
            firstTime = false;
        }
        if (collision.gameObject.tag == "Raspberry")
        {
            if (!hasSpawned)
            {
                Vector2 estimate = (transform.position + collision.transform.position) / 2;
                unityEvents[EventNames.SpawnLemon].Invoke(estimate, points);
                collision.gameObject.GetComponent<Raspberry>().hasSpawned = true;
                hasSpawned = true;
            }
            EventManager.RemoveInvoker(EventNames.SpawnLemon, this);
            Destroy(gameObject);
        }
    }

    #endregion
}