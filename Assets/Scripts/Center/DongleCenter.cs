using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DongleCenter : MonoBehaviour, ISubject
{
    [Header("동글 Prefab")]
    [SerializeField] private GameObject donglePrefab;

    [Header("Guide Line")]
    [SerializeField] private GuideLine guideLine;

    [Header("Particle 프리팹")]
    [SerializeField] private GameObject particlePrefab;

    private DongleController dongle;
    private int nextDongleLevel;
    private bool isGameOver = false;

    public delegate void DongleHandler(DongleController controller);
    public event DongleHandler OnGetController;

    public List<IObserver> levelObserverList = new List<IObserver>();

    private void Start() 
    {
        InitializeCenter();  

        DongleEvents.OnCreateParticle += CreateParticle;
    }

    public void InitializeCenter()
    {
        // 동글이 초기화
        ObjectPool.Instance.InitializePool(20, donglePrefab, PoolTypeEnums.DONGLE);

        // 파티클 초기화
        ObjectPool.Instance.InitializePool(5, particlePrefab, PoolTypeEnums.PARTICLE);

        // 게임 상태 초기화
        SetNextDongleLevel();
        CreateNextDongle();    
    }

    private void CreateNextDongle()
    {
        // 게임 오버 상태라면 새로운 동글을 만들지 않음
        if (isGameOver)
        {
            Debug.Log("GAme OVer ㅎㅎ");
            return;
        }

        DongleController newDongle = DongleFactory.CreateDongle(donglePrefab, nextDongleLevel);
        newDongle.AddObserver(guideLine);
        newDongle.SetDonglePosition(new Vector2(0, GameConstants.DONGLE_HEIGHT));
        newDongle.rigid.simulated = false;

        dongle = newDongle;

        guideLine.EnableLine(true);

        SetNextDongleLevel();

        // OnGetController 이벤트 호출
        OnGetController?.Invoke(dongle);

        StartCoroutine(WaitNext(1f));
    }

    private void SetNextDongleLevel()
    {
        // 동글이 레벨을 1~5 사이에서 랜덤하게 설정
        nextDongleLevel = Random.Range(1, 5);
        NotifyObservers(); 
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

        // 게임 오버 상태라면 동글을 더 이상 생성하지 않음
        if (isGameOver)
        {
            Debug.Log("게임 오버 상태 삐빅");
            yield break;
        }

        yield return new WaitForSeconds(waitTime);

        CreateNextDongle();
    }

    public void DropDongle()
    {
        dongle.RemoveObserver(guideLine);
        guideLine.EnableLine(false);
        dongle = null;
    }

    public void ResetDongles()
    {
        DongleController[] dongleList = FindObjectsByType<DongleController>(FindObjectsSortMode.None);
        
        for (int i = 0; i < dongleList.Length; i++)
        {
            ObjectPool.Instance.ReturnToPool(dongleList[i].gameObject, PoolTypeEnums.DONGLE);
        }

        StopAllCoroutines(); 
        dongle = null; 
    }

    public void SetGameOverState(bool gameOver)
    {
        isGameOver = gameOver;
    }

    public void CreateParticle(Vector2 position, int level)
    {
        GameObject particle = ObjectPool.Instance.GetFromPool(particlePrefab, PoolTypeEnums.PARTICLE);
        particle.transform.position = position;

        MergeEffect mergeEffect = particle.GetComponent<MergeEffect>();
        mergeEffect.SetProperties(level);
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
