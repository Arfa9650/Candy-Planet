using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cherry : IntEventInvoker
{
    #region Fields

    public bool hasSpawnedStrawberry = false;

    #endregion

    #region Methods

    private void Start()
    {
        unityEvents.Add(EventNames.SpawnStrawberry, new StrawberryEvent());
        EventManager.AddInvoker(EventNames.SpawnStrawberry, this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Cherry")
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
        }
    }
    
    #endregion
}
