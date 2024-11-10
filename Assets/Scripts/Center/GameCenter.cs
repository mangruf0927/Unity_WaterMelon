using UnityEngine;

public class GameCenter : MonoBehaviour
{
    [Header("게임 Score")]
    [SerializeField]    private ScoreData scoreData;

    [Header("Dongle Center")]
    [SerializeField]    private DongleCenter dongleCenter;

    [Header("GameOverUI")]
    [SerializeField]    private GameObject gameOverUI;

    [Header("GameExitPopUP")]
    [SerializeField]    private GameObject gameExitUI;


    private bool isGameOver = false;

    private void Start() 
    {
        // 프레임(FPS) 설정 속성 
        Application.targetFrameRate = 60;    
        InitializeGame();

        DongleEvents.OnMerge += AddScore;
        DongleEvents.OnGameOver += GameOver;
        DongleEvents.OnRestart += RestartGame;
        DongleEvents.OnGameExit += ExitGame;
    }

    private void InitializeGame()
    {
        isGameOver = false;
        scoreData.ResetScore();
    } 

    private void AddScore(int score)
    {
        if(!isGameOver)
        {
            scoreData.AddScore(score);
        }
    }

    private void GameOver()
    {
        if(!isGameOver)
        {
            isGameOver = true;
            dongleCenter.SetGameOverState(true);

            gameOverUI.SetActive(true);
            scoreData.GameOver();

            HideNextDongle();
        }
    }

    private void HideNextDongle()
    {
        DongleController[] dongleList = FindObjectsByType<DongleController>(FindObjectsSortMode.None);
        
        for (int i = 0; i < dongleList.Length; i++)
        {
            if(dongleList[i].rigid.simulated == false)
                ObjectPool.Instance.ReturnToPool(dongleList[i].gameObject, PoolTypeEnums.DONGLE);
        }

        dongleCenter.DrawGuideLine(false);
    }

    private void RestartGame()
    {
        dongleCenter.SetGameOverState(false);
        dongleCenter.ResetDongles();
        scoreData.ResetScore();

        isGameOver = false;
    }

    private void ExitGame()
    {
        gameExitUI.SetActive(true);
    }
}
