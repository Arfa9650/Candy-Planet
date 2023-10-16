using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IntEventInvoker
{
    #region Fields

    [SerializeField]
    GameObject strawberry;

    #endregion

    #region Methods

    private void Awake()
    {
        EventManager.Initialize();
    }

    private void Start()
    {
        EventManager.AddListener(EventNames.SpawnStrawberry, SpawnStrawberry);
    }

    public void SpawnStrawberry(Vector2 estimate)
    {
        Instantiate(strawberry, estimate, Quaternion.identity);
    }

    #endregion
}
