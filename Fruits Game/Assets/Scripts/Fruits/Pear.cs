using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pear : IntEventInvoker
{
    #region Fields

    int points = 28;

    public bool hasSpawned = false;

    public bool available = true;

    bool firstTime = true;

    bool nepoBaby = false;

    Rigidbody2D rb2d;

    #endregion

    #region Methods

    private void Start()
    {
        unityEvents.Add(EventNames.SpawnCantaloupe, new CantaloupeEvent());
        EventManager.AddInvoker(EventNames.SpawnCantaloupe, this);
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        if (Spawner.isAttached && transform.parent != null)
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
            Spawner.collided = true;
            firstTime = false;
        }
        if (collision.gameObject.tag == "Pear")
        {
            if (!hasSpawned && collision.gameObject.GetComponent<Pear>().available)
            {
                Vector2 estimate = (transform.position + collision.transform.position) / 2;
                unityEvents[EventNames.SpawnCantaloupe].Invoke(estimate, points);
                collision.gameObject.GetComponent<Pear>().hasSpawned = true;
                collision.gameObject.GetComponent<Pear>().available = false;
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

                EventManager.RemoveInvoker(EventNames.SpawnCantaloupe, this);
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
