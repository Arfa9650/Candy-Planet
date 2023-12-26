using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Dan.Main;

public class GameManager : IntEventInvoker
{
    #region Fields

    Dictionary<FruitNames, GameObject> fruits = new Dictionary<FruitNames, GameObject>();
    
    [SerializeField]
    GameObject MainCanvas;
    
    [SerializeField]
    GameObject GameOverCanvas;
    

    [SerializeField]
    TextMeshProUGUI Score;

    [SerializeField]
    TextMeshProUGUI HighScore;

    [SerializeField]
    TextMeshProUGUI GOScore;
    
    [SerializeField]
    TextMeshProUGUI GOHScore;
    
    [SerializeField]
    GameObject GOhScoreAlert;

    [SerializeField]
    GameObject redline;

    [SerializeField]
    public Image nextFruit;

    public UnityEvent<int> nextCandyEvent;

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
            HighScore.text = "BEST: " + PlayerPrefs.GetInt("High Score").ToString();
        }
        else
        {
            HighScore.text = "BEST: 0";
        }


        fruits.Add(FruitNames.Cherry, Resources.Load(@"Prefabs\1_Cherry") as GameObject);
        fruits.Add(FruitNames.Strawberry, Resources.Load(@"Prefabs\2_Strawberry") as GameObject);
        fruits.Add(FruitNames.Raspberry, Resources.Load(@"Prefabs\3_Raspberry") as GameObject);
        fruits.Add(FruitNames.Lemon, Resources.Load(@"Prefabs\4_Lemon") as GameObject);
        fruits.Add(FruitNames.Orange, Resources.Load(@"Prefabs\5_Orange") as GameObject);
        fruits.Add(FruitNames.Apple, Resources.Load(@"Prefabs\6_Apple") as GameObject);
        fruits.Add(FruitNames.Pear, Resources.Load(@"Prefabs\7_Pear") as GameObject);
        fruits.Add(FruitNames.Cantaloupe, Resources.Load(@"Prefabs\8_Cantaloupe") as GameObject);
        fruits.Add(FruitNames.Pineapple, Resources.Load(@"Prefabs\9_Pineapple") as GameObject);
        fruits.Add(FruitNames.Coconut, Resources.Load(@"Prefabs\10_Coconut") as GameObject);
        fruits.Add(FruitNames.Watermelon, Resources.Load(@"Prefabs\11_Watermelon") as GameObject);

        EventManager.AddListener(EventNames.SpawnRandom, SpawnRandom);
        EventManager.AddListener(EventNames.SpawnCherry, SpawnCherry);
        EventManager.AddListener(EventNames.SpawnStrawberry, SpawnStrawberry);
        EventManager.AddListener(EventNames.SpawnRaspberry, SpawnRaspberry);
        EventManager.AddListener(EventNames.SpawnLemon, SpawnLemon);
        EventManager.AddListener(EventNames.SpawnOrange, SpawnOrange);
        EventManager.AddListener(EventNames.SpawnApple, SpawnApple);
        EventManager.AddListener(EventNames.SpawnPear, SpawnPear);
        EventManager.AddListener(EventNames.SpawnCantaloupe, SpawnCantaloupe);
        EventManager.AddListener(EventNames.SpawnPineapple, SpawnPineapple);
        EventManager.AddListener(EventNames.SpawnCoconut, SpawnCoconut);
        EventManager.AddListener(EventNames.SpawnWatermelon, SpawnWatermelon);
        EventManager.AddListener(EventNames.GameOver, GameOver);

        next = Random.Range(0, 5);
    }

    public void GameOver(Vector2 zero, int Zero)
    {
        redline.SetActive(true);

        Time.timeScale = 0;

        GOScore.text = "SCORE: " + score.ToString();
        GOHScore.text = "BEST: " + PlayerPrefs.GetInt("High Score").ToString();

        //StartCoroutine(Wait());

        AudioManager.audioSource.volume = 0.4f;

        MainCanvas.SetActive(false);
        GameOverCanvas.SetActive(true);

        if (!PlayerPrefs.HasKey("High Score"))
        {
            GOhScoreAlert.SetActive(true);
            PlayerPrefs.SetInt("High Score", score);
            LeaderboardCreator.UploadNewEntry("655147820f3f5a4564be030d64bcd468a770a33713d733713ad870041bafd3c4", PlayerPrefs.GetString("Username"), score, ((msg) => {}));
        }
        else if (score > PlayerPrefs.GetInt("High Score"))
        {
            GOhScoreAlert.SetActive(true);
            PlayerPrefs.SetInt("High Score", score);
            LeaderboardCreator.UploadNewEntry("655147820f3f5a4564be030d64bcd468a770a33713d733713ad870041bafd3c4", PlayerPrefs.GetString("Username"), score, ((msg) => { }));
        }

        
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

        //Debug.Log("Spawned" + current.ToString());

        nextCandyEvent.Invoke(current);

        Instantiate(fruits[(FruitNames)current], estimate, Quaternion.identity, transform);

        next = Random.Range(0, 5);

        nextFruit.sprite = fruits[(FruitNames)next].GetComponent<SpriteRenderer>().sprite;
        nextFruit.SetNativeSize();
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
        Score.text = score.ToString();

        AudioManager.Play(AudioClipNames.Merge);
    }

    public void SpawnStrawberry(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        AudioManager.Play(AudioClipNames.Merge);
        Instantiate(fruits[FruitNames.Strawberry], estimate, Quaternion.identity);
    }
    
    public void SpawnRaspberry(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        AudioManager.Play(AudioClipNames.Merge);
        Instantiate(fruits[FruitNames.Raspberry], estimate, Quaternion.identity);
    }
    
    public void SpawnLemon(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        AudioManager.Play(AudioClipNames.Merge);
        Instantiate(fruits[FruitNames.Lemon], estimate, Quaternion.identity);
    }
    
    public void SpawnOrange(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        AudioManager.Play(AudioClipNames.Merge);
        Instantiate(fruits[FruitNames.Orange], estimate, Quaternion.identity);
    }
    
    public void SpawnApple(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        AudioManager.Play(AudioClipNames.Merge);
        Instantiate(fruits[FruitNames.Apple], estimate, Quaternion.identity);
    }
    
    public void SpawnPear(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        AudioManager.Play(AudioClipNames.Merge);
        Instantiate(fruits[FruitNames.Pear], estimate, Quaternion.identity);
    }
    
    public void SpawnCantaloupe(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        AudioManager.Play(AudioClipNames.Merge);
        Instantiate(fruits[FruitNames.Cantaloupe], estimate, Quaternion.identity);
    }
    
    public void SpawnPineapple(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        AudioManager.Play(AudioClipNames.Merge);
        Instantiate(fruits[FruitNames.Pineapple], estimate, Quaternion.identity);
    }
    
    public void SpawnCoconut(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        AudioManager.Play(AudioClipNames.Merge);
        Instantiate(fruits[FruitNames.Coconut], estimate, Quaternion.identity);
    }
    
    public void SpawnWatermelon(Vector2 estimate, int points)
    {
        score += points;
        Score.text = score.ToString();
        AudioManager.Play(AudioClipNames.Merge);
        Instantiate(fruits[FruitNames.Watermelon], estimate, Quaternion.identity);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 0.5f;
    }

    #endregion
}
