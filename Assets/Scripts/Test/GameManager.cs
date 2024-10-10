using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dongle lastDongle;
    public GameObject donglePrefab;
    public Transform dongleGroup;

    public GameObject effectPrefab;
    public Transform effectGroup;
    
    public int maxLevel;
    public int score;

    public bool isOver = false;

    private void Awake() 
    {
        // 프레임(FPS) 설정 속성 
        Application.targetFrameRate = 60;    
    }

    private void Start() 
    {
        NextDongle();
    }

    private Dongle GetDongle()
    {
        // 이펙트 생성
        GameObject instantEffectObject = Instantiate(effectPrefab, effectGroup);
        ParticleSystem instantEffect = instantEffectObject.GetComponent<ParticleSystem>();

        // 동글 생성
        GameObject instantDongleObject = Instantiate(donglePrefab, dongleGroup);
        Dongle instantDongle = instantDongleObject.GetComponent<Dongle>();
        instantDongle.effect = instantEffect;

        return instantDongle;
    }

    private void NextDongle()
    {
        if(isOver) return;

        Dongle newDongle = GetDongle();
        lastDongle = newDongle;
        lastDongle.gameManager = this;
        lastDongle.level = Random.Range(1, maxLevel);
        lastDongle.gameObject.SetActive(true);
        StartCoroutine(WaitNext());
    }

    IEnumerator WaitNext()
    {
        while(lastDongle != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);

        NextDongle();
    }

    public void TouchDown()
    {
        if(lastDongle == null) return;

        lastDongle.Drag(); 
    }

    public void TouchUp()
    {
        if(lastDongle == null) return;

        lastDongle.Drop();
        lastDongle = null;
    }

    public void GameOver()
    {
        if(isOver) return;

        isOver = true;
        // Debug.Log("게임오버");

        StartCoroutine("GameOverRoutine");
    }

    IEnumerator GameOverRoutine()
    {
        // 화면 안에 활성화 되어있는 모든 동글 가져오기
        Dongle[] dongles = FindObjectsByType<Dongle>(FindObjectsSortMode.None);

        // 지우기 전에 모든 동글의 물리효과 비활성화
        for(int i = 0; i < dongles.Length; i++)
        {
            dongles[i].rigid.simulated = false;
        }

        // 1번의 목록을 하나씩 접근해서 지우기
        for(int i = 0; i < dongles.Length; i++)
        {
            dongles[i].Hide(Vector3.up * 100);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
