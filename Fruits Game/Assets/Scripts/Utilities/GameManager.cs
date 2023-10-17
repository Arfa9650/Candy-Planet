using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IntEventInvoker
{
    #region Fields

    Dictionary<FruitNames, GameObject> fruits = new Dictionary<FruitNames, GameObject>();

    //[SerializeField]
    //GameObject strawberry;GameObject cherry;

    #endregion

    #region Methods

    private void Awake()
    {
        EventManager.Initialize();
    }

    private void Start()
    {
        fruits.Add(FruitNames.Cherry, Resources.Load(@"Prefabs\Cherry") as GameObject);
        fruits.Add(FruitNames.Strawberry, Resources.Load(@"Prefabs\Strawberry") as GameObject);

        EventManager.AddListener(EventNames.SpawnCherry, SpawnCherry);
        EventManager.AddListener(EventNames.SpawnStrawberry, SpawnStrawberry);
    }

    public void SpawnCherry(Vector2 estimate)
    {
        Instantiate(fruits[FruitNames.Cherry], estimate, Quaternion.identity);
    }

    public void SpawnStrawberry(Vector2 estimate)
    {
        Instantiate(fruits[FruitNames.Strawberry], estimate, Quaternion.identity);
    }

    #endregion
}
