using UnityEngine;
using System.Collections.Generic;
 

[CreateAssetMenu(fileName = "ScoreData")]
public class ScoreData : ScriptableObject, ISubject
{
    private int currentScore;
    public List<IObserver> scoreObserverList = new List<IObserver>();

    public void AddScore(int score)
    {
        currentScore += score;
        NotifyObservers();
        // Debug.Log("Current Score: " + currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        NotifyObservers();
    }

    public int GetScore()
    {
        return currentScore;
    }

    // >> 
    public void AddObserver(IObserver observer)
    {
        if (!scoreObserverList.Contains(observer))
        {
            scoreObserverList.Add(observer);
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        if (scoreObserverList.Contains(observer))
        {
            scoreObserverList.Remove(observer);
        }
    }

    public void NotifyObservers()
    {
        foreach (IObserver observer in scoreObserverList)
        {
            observer.Notify(this);
        }
    }
}