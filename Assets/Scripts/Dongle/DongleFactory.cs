using UnityEngine;

public class DongleFactory
{
    private GameObject donglePrefab;
    private Transform dongleGroup;

    public DongleFactory(GameObject prefab, Transform dongle)
    {
        donglePrefab = prefab;
        dongleGroup = dongle;
    }

    public DongleController CreateDongle(int level)
    {
        GameObject newDongle = Object.Instantiate(donglePrefab, dongleGroup);  // 새로운 동글이 생성
        newDongle.SetActive(true);
        
        // DongleController 가져오기
        DongleController dongleController = newDongle.GetComponent<DongleController>();  

        dongleController.dongleLevel = level;  // 레벨 설정
        dongleController.PlayAnimation(level);  // 레벨에 맞는 애니메이션 실행

        return dongleController;
    }
}