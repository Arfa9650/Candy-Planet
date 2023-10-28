using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Spawner : IntEventInvoker
{
    #region Fields

    Dictionary<FruitNames, GameObject> fruits = new Dictionary<FruitNames, GameObject>();

    Vector2 position;

    bool canSpawn = true;
    bool touched = false;
    public static bool isAttached = true;
    public static bool collided = true;

    #endregion

    #region Methods

    /// <summary>
    /// Can invoke the spawn random fruits method
    /// </summary>
    private void Start()
    {
        unityEvents.Add(EventNames.SpawnRandom, new RandomEvent());
        EventManager.AddInvoker(EventNames.SpawnRandom, this); 
    }

    /// <summary>
    /// Spawns if the screen isn't touched(default false) and the fruit touched something(default true) and then inverses them
    /// Can only use touch if no spawns are occuring and has already spawned one fruit
    /// 'isAttached' is a static variable used to help in the generic fruits scripts
    /// </summary>
    private void Update()
    {
        if (canSpawn && !touched && collided)
        {
            isAttached = true;
            unityEvents[EventNames.SpawnRandom].Invoke(transform.position, 0);
            canSpawn = false;
            collided = false;
        }

        if (Input.touchCount > 0 && !canSpawn && !collided)
        {
            position.y = transform.position.y;
            position.x = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x;
            transform.position = position;

            touched = true;

            for (var i = 0; i < Input.touchCount; i++)
            {

                collided = false;

                if (Input.GetTouch(i).phase == TouchPhase.Moved && i == 0)
                {
                    position.x = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x;
                    transform.position = position;
                }
            }
        }

        if(Input.touchCount == 0 && !canSpawn && touched)
        {
            isAttached = false;
            canSpawn = true;
            touched = false;
            collided = false;
        }
    }

    #endregion
}
