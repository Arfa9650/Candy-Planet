using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon : IntEventInvoker
{
    #region Fields

    int points = 33;

    #endregion

    #region Methods

    private void Start()
    {
        unityEvents.Add(EventNames.SpawnCherry, new CherryEvent());
        EventManager.AddInvoker(EventNames.SpawnCherry, this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Watermelon")
        {
            Vector2 estimate = (transform.position + collision.transform.position) / 2;

            GetComponent<CapsuleCollider2D>().enabled = false;

            unityEvents[EventNames.SpawnCherry].Invoke(estimate, points);

            GetComponent<Animator>().SetTrigger("Exit");
            
            EventManager.RemoveInvoker(EventNames.SpawnCherry, this);
            Invoke("KillTarget", 0.1f);
        }
    }

    void KillTarget()
    {
        Destroy(gameObject);
    }
    #endregion
}
