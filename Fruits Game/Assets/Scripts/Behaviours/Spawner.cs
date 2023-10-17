using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : IntEventInvoker
{
    #region Fields

    Dictionary<FruitNames, GameObject> fruits = new Dictionary<FruitNames, GameObject>();

    Vector2 position;

    #endregion

    #region Methods

    private void Start()
    {
        unityEvents.Add(EventNames.SpawnCherry, new CherryEvent());
        EventManager.AddInvoker(EventNames.SpawnCherry, this); 

        unityEvents.Add(EventNames.SpawnStrawberry, new StrawberryEvent());
        EventManager.AddInvoker(EventNames.SpawnStrawberry, this);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            position.y = transform.position.y;
            position.x = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x;
            transform.position = position;

            for (var i = 0; i < Input.touchCount; i++)
            {


                if (Input.GetTouch(i).phase == TouchPhase.Moved && i == 0)
                {
                    position.x = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x;
                    //position.z = 0;
                    transform.position = position;
                }
            }
        }
    }

    private void OnMouseUp()
    {
        
    }

    void SpawnRandom()
    {
        int rand = UnityEngine.Random.Range(0, 2);

        unityEvents[(EventNames)rand].Invoke(transform.position);
    }

    #endregion
}
