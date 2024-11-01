using UnityEngine;

public class GameCenter : MonoBehaviour
{
    [Header("게임 Score")]
    [SerializeField]    private ScoreData scoreData;

    [Header("Dongle Center")]
    [SerializeField]    private DongleCenter dongleCenter;

    [Header("GameOverUI")]
    [SerializeField]    private GameObject gameOverUI;


    private bool isGameOver = false;

    private void Start() 
    {
        // 프레임(FPS) 설정 속성 
        Application.targetFrameRate = 60;    
        InitializeGame();

        DongleEvents.OnMerge += AddScore;
        DongleEvents.OnGameOver += GameOver;
        DongleEvents.OnRestart += RestartGame;
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

            gameOverUI.SetActive(true);
        }
    }

    public void RestartGame()
    {
        dongleCenter.ResetDongles();
        scoreData.ResetScore();
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

}
