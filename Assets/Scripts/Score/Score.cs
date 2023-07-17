[System.Serializable]
public class Score
{
    public string name;
    public int score;

    public Score(string playerName, int scoreValue)
    {
        name = playerName;
        score = scoreValue;
    }
}
