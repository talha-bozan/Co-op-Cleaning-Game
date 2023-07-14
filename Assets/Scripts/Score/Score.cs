using System;

[Serializable]
public class Score
{
    public string name;
    public int score;

    public Score(string playerName, int playerScore)
    {
        name = playerName;
        score = playerScore;
    }
}
