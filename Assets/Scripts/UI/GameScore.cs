using TMPro;
using UnityEngine;

public class GameScore : MonoBehaviour, IObserver
{
    [SerializeField]    private TextMeshProUGUI scoreText;
    [SerializeField]    private TextMeshProUGUI finalScoreText;


    public void Notify(ISubject subject)
    {
        if(subject is ScoreData score)
        {
            if(!score.isGameOver)
                UpdateScoreUI(score);
            else
                ShowFinalScore(score);
        }
    }

    private void UpdateScoreUI(ScoreData score)
    {
        scoreText.text = score.GetScore().ToString();
        // Debug.Log(score.GetScore());
    }

    private void ShowFinalScore(ScoreData score)
    {
        finalScoreText.text = score.GetFinalScore().ToString();
    }
}
