using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text textScore;
    int score;

    public int GetScore { get => score; }

    void Awake()
    {
        textScore = GetComponent<Text>();
    }

    public void SetScore(int points)
    {
        if(points > 0)
        {
            score = points;
            textScore.text = $"x {GetScore}";
        }
    }
    public void AddPoints(int points)
    {
        if(points > 0)
        {
            score += points;
            textScore.text = $"x {GetScore}";
        }
    }

    public void ResetScore()
    {
        score = 0;
        textScore.text = $"x {GetScore}";
    }
}
