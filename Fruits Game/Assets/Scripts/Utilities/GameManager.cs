using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : IntEventInvoker
{
    #region Fields

    Dictionary<FruitNames, GameObject> fruits = new Dictionary<FruitNames, GameObject>();
    
    [SerializeField]
    TextMeshProUGUI Score;

    [SerializeField]
    TextMeshProUGUI HighScore;

    [SerializeField]
    public Image nextFruit;

    int score = 0;

    int current;
    int next;

    #endregion

    #region Methods

    /// <summary>
    /// Sets Frame Rate and Initializes all static clases
    /// </summary>
    private void Awake()
    {
        Application.targetFrameRate = 61;
        EventManager.Initialize();
    }

    /// <summary>
    /// Sets score and High score UI
    /// Loads all fruits prefabs
    /// Listens for all the events
    /// Sets the intial 'next' value so it can be used to project next fruit to be spawned
    /// </summary>
    private void Start()
    {
        Score.text = score.ToString();
        if(PlayerPrefs.HasKey("High Score"))
        {
            HighScore.text = PlayerPrefs.GetInt("High Score").ToString();
        }
        else
        {
            HighScore.text = "0";
        }


        fruits.Add(FruitNames.Cherry, Resources.Load(@"Prefabs\Cherry") as GameObject);
        fruits.Add(FruitNames.Strawberry, Resources.Load(@"Prefabs\Strawberry") as GameObject);
        fruits.Add(FruitNames.Raspberry, Resources.Load(@"Prefabs\Raspberry") as GameObject);
        fruits.Add(FruitNames.Lemon, Resources.Load(@"Prefabs\Lemon") as GameObject);

        EventManager.AddListener(EventNames.SpawnRandom, SpawnRandom);
        EventManager.AddListener(EventNames.SpawnCherry, SpawnCherry);
        EventManager.AddListener(EventNames.SpawnStrawberry, SpawnStrawberry);
        EventManager.AddListener(EventNames.SpawnRaspberry, SpawnRaspberry);
        EventManager.AddListener(EventNames.SpawnLemon, SpawnLemon);

        next = Random.Range(0, 2);
    }


    /// <summary>
    /// Spawns a random Fruit from the 'FruitNames' enum
    /// No points are added to the score because its used by the spawner mechanic, not the merging mechanic
    /// Sets 'next' value after spawn so we can project next fruit
    /// </summary>
    /// <param name="estimate"></param>
    /// <param name="points"></param>
    public void SpawnRandom(Vector2 estimate, int points = 0)
    {
        current = next;

        Instantiate(fruits[(FruitNames)current], estimate, Quaternion.identity, transform);

        next = Random.Range(0, 3);

        nextFruit.sprite = fruits[(FruitNames)next].GetComponent<SpriteRenderer>().sprite;
    }    

    /// <summary>
    /// Beyond this all the fuctions work the same
    /// Spawns fruit relative to their name and Adds points accordingly
    /// These fuctions are used for merging fruits and rewarding point
    /// Should have used another parameter in the event system to merge all these in one fuction but too late now
    /// </summary>
    /// <param name="estimate"></param>
    /// <param name="points"></param>
    public void SpawnCherry(Vector2 estimate, int points)
    {
        score += points;
        Instantiate(fruits[FruitNames.Cherry], estimate, Quaternion.identity);
    }

    public void SpawnStrawberry(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        Instantiate(fruits[FruitNames.Strawberry], estimate, Quaternion.identity);
    }
    
    public void SpawnRaspberry(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        Instantiate(fruits[FruitNames.Raspberry], estimate, Quaternion.identity);
    }
    
    public void SpawnLemon(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        Instantiate(fruits[FruitNames.Lemon], estimate, Quaternion.identity);
    }

    #endregion
}
