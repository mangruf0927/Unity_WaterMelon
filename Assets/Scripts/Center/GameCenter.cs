using UnityEngine;

public class GameCenter : MonoBehaviour
{
    [Header("게임 Score")]
    [SerializeField]    private ScoreData scoreData;

    [Header("Dongle Center")]
    [SerializeField]    private DongleCenter dongleCenter;

    private bool isGameOver = false;

    private void Start() 
    {
        // 프레임(FPS) 설정 속성 
        Application.targetFrameRate = 60;    
        InitializeGame();

        DongleEvents.OnMerge += AddScore;
        DongleEvents.OnGameOver += GameOver;
    }

    public void InitializeGame()
    {
        isGameOver = false;
        scoreData.ResetScore();
    } 

    public void AddScore(int score)
    {
        if(!isGameOver)
        {
            scoreData.AddScore(score);
        }
    }

    public void GameOver()
    {
        if(!isGameOver)
        {
            isGameOver = true;
            dongleCenter.SetGameOverState(true);

            ResetDongles();

            Debug.Log("게임 오버 !!!!!!");
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void ResetDongles()
    {
        DongleController[] dongleList = FindObjectsByType<DongleController>(FindObjectsSortMode.None);
        
        for (int i = 0; i < dongleList.Length; i++)
        {
            ObjectPool.Instance.ReturnToPool(dongleList[i].gameObject, PoolTypeEnums.DONGLE);
        }

        StopAllCoroutines(); 
    }
}
