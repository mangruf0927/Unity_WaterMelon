using UnityEngine;

public class HudCenter : MonoBehaviour
{
    [Header("동글 Score UI")]
    [SerializeField]    private ScoreData scoreData;
    [SerializeField]    private GameScore gameScore;

    private void Start() 
    {
        scoreData.AddObserver(gameScore);    
    }
}
