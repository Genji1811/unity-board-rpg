using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
{
    var scores = LeaderboardManager.instance.GetScores();

    scoreText.text = "TOP 5\n\n";

    for (int i = 0; i < 5; i++)
    {
        if (i < scores.Count)
        {
            scoreText.text +=
                "#" + (i + 1) +
                "  " + scores[i] +
                " turns\n";
        }
        else
        {
            scoreText.text +=
                "#" + (i + 1) +
                "  ---\n";
        }
    }
}
}