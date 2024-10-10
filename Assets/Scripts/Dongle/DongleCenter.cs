using System.Collections;
using UnityEngine;

public class DongleCenter : MonoBehaviour
{
    [Header("동글 Prefab")]
    [SerializeField] private GameObject donglePrefab;

    private DongleController dongle;

    public delegate void DongleHandler(DongleController controller);
    public event DongleHandler OnGetController;

    private void Start() 
    {
        ObjectPool.Instance.InitializePool(20, donglePrefab, PoolTypeEnums.DONGLE);
        CreateNextDongle();    
    }

    private void CreateNextDongle()
    {
        // 동글이 레벨을 1~4 사이에서 랜덤하게 설정
        int randomLevel = Random.Range(1, 4);
        DongleController newDongle = DongleFactory.CreateDongle(donglePrefab, randomLevel);
        newDongle.transform.position = new Vector3(0, 6.5f, 0);
        newDongle.rigid.simulated = false;
        
        dongle = newDongle;

        // OnGetController 이벤트 호출
        OnGetController?.Invoke(dongle);

        StartCoroutine(WaitNext(2.5f));
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
}
