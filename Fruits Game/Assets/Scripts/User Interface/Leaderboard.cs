using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> userNames;
    [SerializeField]
    private List<TextMeshProUGUI> highScores;
    [SerializeField]
    private TextMeshProUGUI you;

    private string publicLeaderboardKey = "655147820f3f5a4564be030d64bcd468a770a33713d733713ad870041bafd3c4";

    private void Start()
    {
        if(PlayerPrefs.HasKey("High Score"))
        {
            you.text = PlayerPrefs.GetString("Username") + ": " + PlayerPrefs.GetInt("High Score").ToString();
        }
        else
        {
            you.text = PlayerPrefs.GetString("Username") + ": 0";
        }
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < userNames.Count) ? msg.Length : userNames.Count;
            for(int i = 0; i < loopLength; i++)
            {
                userNames[i].text = msg[i].Username;
                highScores[i].text = msg[i].Score.ToString();
            }
            
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry("655147820f3f5a4564be030d64bcd468a770a33713d733713ad870041bafd3c4", username, score, ((msg) => 
        {
            GetLeaderboard();
            you.text = PlayerPrefs.GetString("Username") + ": " + PlayerPrefs.GetInt("High Score").ToString();
        }));
    }
}
