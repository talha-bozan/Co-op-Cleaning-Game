using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private ScoreData sd;
    void Awake()
    {
        var json = PlayerPrefs.GetString("scores", "{}");
        sd = JsonUtility.FromJson<ScoreData>(json);
    }

    public IEnumerable<Score> GetHighScores()
    {
        return sd.scores.OrderByDescending(x => x.score);
    }

    public void AddScore(Score score)
    {
        sd.scores.Add(score);
    }

    private void OnDestroy()
    {
        SaveScore();
    }

    public void SaveScore()
    {
        var json = JsonUtility.ToJson(sd);
        Debug.Log(json);
        PlayerPrefs.SetString("scores", json);
    }

    public void ChangeScore(string playerName, int newScore)
    {
        // Find the score entry for the specified player
        Score playerScore = sd.scores.FirstOrDefault(x => x.name == playerName);

        // If the player's score exists, update it
        if (playerScore != null)
        {
            playerScore.score = newScore;
        }
        // If the player's score doesn't exist, add a new entry
        else
        {
            Score newPlayerScore = new Score(playerName, newScore);
            sd.scores.Add(newPlayerScore);
        }

        SaveScore();
    }
    public void ClearScores()
    {
        sd.scores.Clear();
        SaveScore();
    }


}