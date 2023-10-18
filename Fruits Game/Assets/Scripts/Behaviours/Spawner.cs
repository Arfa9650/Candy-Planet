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

    private void Start()
    {
        unityEvents.Add(EventNames.SpawnRandom, new RandomEvent());
        EventManager.AddInvoker(EventNames.SpawnRandom, this); 
        
        unityEvents.Add(EventNames.SpawnCherry, new CherryEvent());
        EventManager.AddInvoker(EventNames.SpawnCherry, this); 

        unityEvents.Add(EventNames.SpawnStrawberry, new StrawberryEvent());
        EventManager.AddInvoker(EventNames.SpawnStrawberry, this);
    }

    private void Update()
    {
        if (canSpawn && !touched && collided)
        {
            isAttached = true;
            unityEvents[EventNames.SpawnRandom].Invoke(transform.position);
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
