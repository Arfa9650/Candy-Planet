using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Spawner : IntEventInvoker
{
    #region Fields

    [SerializeField]
    GameObject Line;

    Dictionary<FruitNames, GameObject> fruits = new Dictionary<FruitNames, GameObject>();

    bool canSpawn = true;
    bool touched = false;
    bool clamped = false;
    bool clampedNow = false;
    public static bool isAttached = true;
    public static bool collided = true;

    float minX = -1.47f;
    float maxX = 1.47f;

    #endregion

    #region Methods

    /// <summary>
    /// Can invoke the spawn random fruits method
    /// </summary>
    private void Start()
    {
        Line.SetActive(false);
        collided = true;
        unityEvents.Add(EventNames.SpawnRandom, new RandomEvent());
        EventManager.AddInvoker(EventNames.SpawnRandom, this);
        EventManager.AddListener(EventNames.GameOver, SelfDestruct);
    }

    /// <summary>
    /// Spawns if the screen isn't touched(default false) and the fruit touched something(default true) and then inverses them
    /// Can only use touch if no spawns are occuring and has already spawned one fruit
    /// 'isAttached' is a static variable used to help in the generic fruits scripts
    /// </summary>
    private void Update()
    {
        if (canSpawn && collided)
        {
            //Debug.Log("Spawned 1");
            isAttached = true;
            unityEvents[EventNames.SpawnRandom].Invoke(transform.position, 0);
            canSpawn = false;
            collided = false;
        }

        if (Input.touchCount > 0 /*&& !canSpawn && !collided*/)
        {
            if(!canSpawn)
                Line.SetActive(true);
            Vector2 position;
            position.y = transform.position.y;
            position.x = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x;
            position.x = Mathf.Clamp(position.x, minX, maxX);
            if (clamped)
            {
                position.x = Mathf.Clamp(position.x, minX, maxX);
                clampedNow = true;
            }
            transform.position = position;

            touched = true;

            for (var i = 0; i < Input.touchCount; i++)
            {

                //collided = false;
                touched = true;

                if (Input.GetTouch(i).phase == TouchPhase.Moved && i == 0)
                {
                    position.x = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x;
                    position.x = Mathf.Clamp(position.x, minX, maxX);
                    if (clamped)
                    {
                        position.x = Mathf.Clamp(position.x, minX, maxX);
                        clampedNow = true;
                    }
                    transform.position = position;
                }
            }
        }

        if (Input.touchCount == 0 && !canSpawn && touched && clampedNow)
        {
            Line.SetActive(false);

            //Debug.Log("Can Spawn Now");
            //AudioManager.Play(AudioClipNames.Drop);
            isAttached = false;
            canSpawn = true;
            touched = false;
            //collided = false;
            clamped = false;
            clampedNow = false;
        }
        else if (Input.touchCount == 0)
        {
            Line.SetActive(false);

            touched = false;
        }
    }

    public void SetMinMax(int candyNum)
    {
        switch(candyNum)
        {
            case 0:
                minX = -1.72f;
                maxX = 1.72f;
                break;
            case 1:
                minX = -1.66f;
                maxX = 1.66f;
                break;
            case 2:
                minX = -1.59f;
                maxX = 1.59f;
                break;
            case 3:
                minX = -1.58f;
                maxX = 1.58f;
                break;
            case 4:
                minX = -1.51f;
                maxX = 1.51f;
                break;
        }
        clamped = true;
    }

    public void SelfDestruct(Vector2 zero, int nope)
    {
        Destroy(gameObject);
    }

    #endregion
}
