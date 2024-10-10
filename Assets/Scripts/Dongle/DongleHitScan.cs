using UnityEngine;

public class DongleHitScan : MonoBehaviour
{
    [Header("동글이 컨트롤러")]
    [SerializeField] DongleController dongleController;

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Dongle"))
        {
            Debug.Log("동글이와 충돌");
            DongleController dongle = other.gameObject.GetComponent<DongleController>();

            if(dongleController.dongleLevel == dongle.dongleLevel && 
                !dongleController.isMerge && !dongle.isMerge &&
                dongleController.dongleLevel < dongleController.dongleMaxLevel)
            {
                Debug.Log("충돌 동글이와 내 동글이 레벨 같음 데헷");
                float myX = transform.position.x;
                float otherX = other.transform.position.x;

                if(myX < otherX)
                {
                    Debug.Log("내 엑스가 더 작넹 ");
                    MergeDongles(dongle, other.contacts[0].point);
                }
            }
        }
    }

    private void MergeDongles(DongleController otherDongle, Vector2 collisionPoint)
    {
        dongleController.isMerge = true;
        otherDongle.isMerge = true;

        // Rigidbody 물리 연산을 중지
        dongleController.GetComponent<Rigidbody2D>().simulated = false;
        otherDongle.GetComponent<Rigidbody2D>().simulated = false;

        // 두 동글이를 제거
        Destroy(otherDongle.gameObject);
        Destroy(dongleController.gameObject);

        // 새로운 동글이 생성 (레벨 + 1)
        // DongleController newDongle = dongleFactory.CreateDongle(dongleController.dongleLevel + 1);
        // newDongle.transform.position = collisionPoint; // 충돌 지점에 생성
        Debug.Log(collisionPoint + "에 " + (dongleController.dongleLevel + 1) + "레벨 동글 생성");
    }
}
