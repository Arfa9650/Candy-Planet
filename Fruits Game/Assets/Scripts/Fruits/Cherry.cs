using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cherry : IntEventInvoker
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
        unityEvents.Add(EventNames.SpawnStrawberry, new StrawberryEvent());
        EventManager.AddInvoker(EventNames.SpawnStrawberry, this);
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        if(Spawner.isAttached && transform.parent != null)
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
        if(!Spawner.isAttached && transform.parent != null)
        {
            transform.parent = null;
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(firstTime && nepoBaby)
        {
            Spawner.collided = true;
            firstTime = false;
        }
        if(collision.gameObject.tag == "Cherry")
        {
            if (!hasSpawned)
            {
                Vector2 estimate = (transform.position + collision.transform.position) / 2;
                unityEvents[EventNames.SpawnStrawberry].Invoke(estimate, points);
                collision.gameObject.GetComponent<Cherry>().hasSpawned = true;
                hasSpawned = true;
            }
            EventManager.RemoveInvoker(EventNames.SpawnStrawberry, this);
            Destroy(gameObject);
        }
    }
    
    #endregion
}
