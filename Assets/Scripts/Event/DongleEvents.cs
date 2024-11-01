using UnityEngine;

public static class DongleEvents
{
    public delegate void DongleMergeHandler(int level);
    public static event DongleMergeHandler OnMerge;

    public delegate void DongleHandler();
    public static event DongleHandler OnGameOver;
    public static event DongleHandler OnRestart;

    public delegate void ParticleHandler(Vector2 position, int level);
    public static event ParticleHandler OnCreateParticle;

    public static void MergeDongle(int level)
    {
        OnMerge?.Invoke(level);
    }

    public static void CreateParticle(Vector2 position, int level)
    {
        OnCreateParticle?.Invoke(position, level);
    }

    public static void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void Restart()
    {
        OnRestart?.Invoke();
    }
}
