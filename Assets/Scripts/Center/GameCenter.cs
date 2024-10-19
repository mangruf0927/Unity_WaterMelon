using UnityEngine;

public class GameCenter : MonoBehaviour
{
    [Header("게임 Score")]
    [SerializeField]    private ScoreData scoreData;

    private bool isGameOver = false;

    private void Start() 
    {
        // 프레임(FPS) 설정 속성 
        Application.targetFrameRate = 60;    
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
            Debug.Log("게임 오버 !!!!!!");
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
