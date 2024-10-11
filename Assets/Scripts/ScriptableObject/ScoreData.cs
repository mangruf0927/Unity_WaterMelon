using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData")]
public class ScoreData : ScriptableObject
{
    private int currentScore;

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Current Score: " + currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}