using UnityEngine;

public class HudCenter : MonoBehaviour
{
    [Header("동글 Score UI")]
    [SerializeField]    private ScoreData scoreData;
    [SerializeField]    private GameScore gameScore;

    [Header("다음 동글 UI")]
    [SerializeField]    private DongleCenter dongleCenter;
    [SerializeField]    private NextDongle nextDongle;

    private void Awake() 
    {
        scoreData.AddObserver(gameScore);    
        dongleCenter.AddObserver(nextDongle);
    }
}
