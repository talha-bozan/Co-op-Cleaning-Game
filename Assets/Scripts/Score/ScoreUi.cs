using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreUi : MonoBehaviour
{
    public RowUi rowUi;
    public ScoreManager scoreManager;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            var row = Instantiate(rowUi, transform).GetComponent<RowUi>();
            row.rank.text = (i + 1).ToString();
            row.name.text = $"Player{i}"; 
            row.score.text = $"{0}";
        }
    }
    public void ChangeScore(string playerID, int score)
    {
        // Find the row with the specified player ID
        RowUi targetRow = transform.GetComponentsInChildren<RowUi>()
            .FirstOrDefault(row => row.name.text == $"Player{playerID}");

        if (targetRow != null)
        {
            // Update the score text of the target row
            targetRow.score.text = score.ToString();
        }
    }

}
