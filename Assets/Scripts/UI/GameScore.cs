using TMPro;
using UnityEngine;

public class GameScore : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void Notify(ISubject subject)
    {
        if(subject is ScoreData score)
        {
            UpdateScoreUI(score);
        }
    }

    public void UpdateScoreUI(ScoreData score)
    {
        scoreText.text = score.GetScore().ToString();
        // Debug.Log(score.GetScore());
    }
}
