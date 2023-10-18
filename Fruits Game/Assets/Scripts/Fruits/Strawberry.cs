using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : IntEventInvoker
{
    #region Fields

    public bool hasSpawnedStrawberry = false;

    bool firstTime = true;
    bool nepoBaby = false;

    #endregion

    #region Methods

    private void Start()
    {
        unityEvents.Add(EventNames.SpawnStrawberry, new StrawberryEvent());
        EventManager.AddInvoker(EventNames.SpawnStrawberry, this);
        if (Spawner.isAttached && transform.parent != null)
        {
            nepoBaby = true;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
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
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
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
        /*if (collision.gameObject.tag == "Cherry")
        {
            if (!hasSpawnedStrawberry)
            {
                Vector2 estimate = (transform.position + collision.transform.position) / 2;
                unityEvents[EventNames.SpawnStrawberry].Invoke(estimate);
                collision.gameObject.GetComponent<Cherry>().hasSpawnedStrawberry = true;
                hasSpawnedStrawberry = true;
            }
            EventManager.RemoveInvoker(EventNames.SpawnStrawberry, this);
            Destroy(gameObject);
        }*/
    }

    #endregion
}
