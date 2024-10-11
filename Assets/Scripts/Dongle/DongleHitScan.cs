using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DongleHitScan : MonoBehaviour
{
    [Header("동글이 컨트롤러")]
    [SerializeField] DongleController dongleController;

    private DongleCenter dongleCenter;

    public void SetDongleCenter(DongleCenter center)
    {
        dongleCenter = center;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Dongle"))
        {
            DongleController dongle = other.gameObject.GetComponent<DongleController>();

            if(dongleController.dongleLevel == dongle.dongleLevel && 
                !dongleController.isMerge && !dongle.isMerge &&
                dongleController.dongleLevel < dongleController.dongleMaxLevel)
            {
                if (dongleController.GetInstanceID() < dongle.GetInstanceID())
                {
                    MergeDongles(dongle, other.contacts[0].point);
                    
                }
            }
        }
    }

    private void MergeDongles(DongleController otherDongle, Vector2 collisionPoint)
    {
        dongleController.isMerge = true;
        otherDongle.isMerge = true;

        dongleController.rigid.simulated = false;
        otherDongle.rigid.simulated = false;

        // 점수 계산
        AddScore(dongleController.dongleLevel);

        // 두 동글이를 제거
        ObjectPool.Instance.ReturnToPool(otherDongle.gameObject, PoolTypeEnums.DONGLE);
        ObjectPool.Instance.ReturnToPool(gameObject, PoolTypeEnums.DONGLE);

        // 새로운 동글이 생성 (레벨 + 1)
        DongleController newDongle = DongleFactory.CreateDongle(gameObject ,dongleController.dongleLevel + 1, dongleCenter);
        newDongle.transform.position = collisionPoint; // 충돌 지점에 생성
        newDongle.rigid.simulated = true;

        // Debug.Log(collisionPoint + "에 " + (dongleController.dongleLevel + 1) + "레벨 동글 생성");
    }

    private void AddScore(int level)
    {
        int score = (level * (level + 1)) / 2;
        dongleCenter.AddScore(score);
    }
}
