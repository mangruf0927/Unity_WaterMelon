using UnityEngine;

public static class DongleEvents
{
    public delegate void DongleIntHandler(int level);
    public static event DongleIntHandler OnMerge;

    public delegate void DongleHandler();
    public static event DongleHandler OnGameOver;

    public static void MergeDongle(int level)
    {
        OnMerge?.Invoke(level);
    }

    public static void GameOver()
    {
        OnGameOver?.Invoke();
    }
}
