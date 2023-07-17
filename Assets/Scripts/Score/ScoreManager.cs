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
    public int GetScore(string playerName)
    {
        // Find the score with the player's name
        Score playerScore = sd.scores.Find(score => score.name == playerName);

        if (playerScore != null)
        {
            return playerScore.score;
        }
        else
        {
            // Player score not found
            return 0; 
        }
    }

    public void UpdateScore(string playerName, int scoreValue)
    {
        // Find the score with the player's name
        Score playerScore = sd.scores.Find(score => score.name == playerName);

        if (playerScore != null)
        {
            // Update the existing score
            playerScore.score = scoreValue;
        }
        else
        {
            // Create a new score for the player
            Score newScore = new Score(playerName, scoreValue);
            sd.scores.Add(newScore);
        }

        // Save the updated scores
        SaveScore();
    }


    public void SaveScore()
    {
        var json = JsonUtility.ToJson(sd);
        PlayerPrefs.SetString("scores", json);
    }

    
    public void ClearScores()
    {
        sd.scores.Clear();
        SaveScore();
    }


}