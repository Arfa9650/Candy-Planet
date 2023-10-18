using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IntEventInvoker
{
    #region Fields

    Dictionary<FruitNames, GameObject> fruits = new Dictionary<FruitNames, GameObject>();

    #endregion

    #region Methods

    private void Awake()
    {
        Application.targetFrameRate = 61;
        EventManager.Initialize();
    }

    private void Start()
    {
        fruits.Add(FruitNames.Cherry, Resources.Load(@"Prefabs\Cherry") as GameObject);
        fruits.Add(FruitNames.Strawberry, Resources.Load(@"Prefabs\Strawberry") as GameObject);

        EventManager.AddListener(EventNames.SpawnRandom, SpawnRandom);
        EventManager.AddListener(EventNames.SpawnCherry, SpawnCherry);
        EventManager.AddListener(EventNames.SpawnStrawberry, SpawnStrawberry);
    }

    public void SpawnRandom(Vector2 estimate)
    {
        int rand = UnityEngine.Random.Range(0, 2);
        Instantiate(fruits[(FruitNames)rand], estimate, Quaternion.identity, transform);
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
