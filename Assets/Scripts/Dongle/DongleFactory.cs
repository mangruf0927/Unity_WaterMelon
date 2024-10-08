using UnityEngine;

public class DongleFactory : MonoBehaviour
{
    private GameObject donglePrefab;
    private DongleController dongleController;

    public DongleFactory(GameObject prefab)
    {
        donglePrefab = prefab;
    }

    public DongleController CreateDongle(int level)
    {
        GameObject newDongle = Instantiate(donglePrefab);  // 새로운 동글이 생성
        dongleController = newDongle.GetComponent<DongleController>();
        dongleController.dongleLevel = level;  // 레벨 설정
        
        return dongleController;
    }
}