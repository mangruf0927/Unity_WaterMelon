using UnityEngine;

public class DongleFactory : MonoBehaviour
{
    [SerializeField] private GameObject donglePrefab;
    [SerializeField] private InputCenter inputCenter; 

    public DongleFactory(GameObject prefab)
    {
        donglePrefab = prefab;
    }

    public void CreateDongle(int level)
    {
        GameObject newDongle = Instantiate(donglePrefab);  // 새로운 동글이 생성
        DongleController dongleController = newDongle.GetComponent<DongleController>();
        dongleController.dongleLevel = level;  // 레벨 설정

        inputCenter.SetDongleController(dongleController);  // InputCenter에 새로운 DongleController 전달
    }
}