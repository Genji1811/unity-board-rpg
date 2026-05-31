using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeaderboardData
{
    public List<int> scores = new List<int>();
}

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager instance;

    private const string SAVE_KEY = "LEADERBOARD_TURNS";
    public int maxScores = 5;

    private LeaderboardData data = new LeaderboardData();

    void Awake()
    {
        if (instance == null)
            instance = this;

        Load();
    }

    public void AddScore(int turnCount)
    {
        data.scores.Add(turnCount);
        data.scores.Sort(); // nhỏ nhất đứng đầu

        if (data.scores.Count > maxScores)
            data.scores.RemoveAt(data.scores.Count - 1);

        Save();
    }

    public List<int> GetScores()
    {
        return data.scores;
    }

    public void ClearScores()
    {
        data.scores.Clear();
        Save();
    }

    void Save()
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    void Load()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            data = JsonUtility.FromJson<LeaderboardData>(json);
        }
    }
}