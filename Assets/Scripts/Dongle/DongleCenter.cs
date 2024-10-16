using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DongleCenter : MonoBehaviour, ISubject
{
    [Header("동글 Prefab")]
    [SerializeField] private GameObject donglePrefab;

    [Header("동글 Score")]
    [SerializeField] private ScoreData scoreData;

    private DongleController dongle;
    private int nextDongleLevel;

    public delegate void DongleHandler(DongleController controller);
    public event DongleHandler OnGetController;

    public List<IObserver> levelObserverList = new List<IObserver>();

    private void Start() 
    {
        // 오브젝트 풀 초기화
        ObjectPool.Instance.InitializePool(20, donglePrefab, PoolTypeEnums.DONGLE);

        // 게임 점수 초기화
        scoreData.ResetScore();

        nextDongleLevel = Random.Range(1, 4);
        CreateNextDongle();    
    }

    private void CreateNextDongle()
    {
        DongleController newDongle = DongleFactory.CreateDongle(donglePrefab, nextDongleLevel, this);
        newDongle.transform.position = new Vector3(0, 5f, 0);
        newDongle.rigid.simulated = false;
        
        dongle = newDongle;

        // 동글이 레벨을 1~5 사이에서 랜덤하게 설정
        nextDongleLevel = Random.Range(1, 5);
        // Debug.Log("다음 동글 레벨 : " + nextDongleLevel);  

        NotifyObservers();     

        // OnGetController 이벤트 호출
        OnGetController?.Invoke(dongle);

        StartCoroutine(WaitNext(1f));
    }

    public int GetNextLevel()
    {
        return nextDongleLevel;
    }

    private IEnumerator WaitNext(float waitTime)
    {
        while(dongle != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);
        CreateNextDongle();
    }

    public void DropDongle()
    {
        dongle = null;
    }

    public void AddScore(int score)
    {
        scoreData.AddScore(score);
    }

    // >> 
    public void AddObserver(IObserver observer)
    {
        if (!levelObserverList.Contains(observer))
        {
            levelObserverList.Add(observer);
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        if (levelObserverList.Contains(observer))
        {
            levelObserverList.Remove(observer);
        }
    }

    public void NotifyObservers()
    {
        foreach (IObserver observer in levelObserverList)
        {
            observer.Notify(this);
        }
    }
}
