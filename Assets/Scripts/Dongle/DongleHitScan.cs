using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DongleHitScan : MonoBehaviour
{
    [Header("동글이 컨트롤러")]
    [SerializeField] DongleController dongleController;

    [Header("동글이 스프라이트")]
    [SerializeField] SpriteRenderer spriteRenderer;

    private Coroutine gameOverRoutine;
    private float elapsedTime = 0f;
    private bool isTouchingBorder;

    // 동글 - 동글 충돌
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Dongle"))
        {
            DongleController dongle = other.gameObject.GetComponent<DongleController>();

            if(dongleController.dongleLevel == dongle.dongleLevel && 
                !dongleController.isMerge && !dongle.isMerge &&
                dongleController.dongleLevel < dongleController.dongleMaxLevel)
            {
                if(gameOverRoutine != null)
                    StopCoroutine(gameOverRoutine);

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

        // 두 동글이를 제거 (Pool에 반환)
        ObjectPool.Instance.ReturnToPool(otherDongle.gameObject, PoolTypeEnums.DONGLE);
        ObjectPool.Instance.ReturnToPool(gameObject, PoolTypeEnums.DONGLE);

        // 새로운 동글이 생성 (레벨 + 1)
        DongleController newDongle = DongleFactory.CreateDongle(gameObject ,dongleController.dongleLevel + 1);
        newDongle.transform.position = collisionPoint; // 충돌 지점에 생성
        newDongle.rigid.simulated = true;
    }

    private void AddScore(int level)
    {
        int score = (level * (level + 1)) / 2;
        DongleEvents.MergeDongle(level);
    }

    // 동글 - GameOver Area 충돌
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("GameOver") && !isTouchingBorder)
        {
            if (dongleController.rigid.simulated == true)
            {
                elapsedTime += Time.deltaTime;

                // 일정 시간 이상 닿아있을 때 색상을 변경
                if (elapsedTime >= 0.5f && gameOverRoutine == null)
                {
                    gameOverRoutine = StartCoroutine(ChangeColor(Color.red));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            isTouchingBorder = true;

            if (dongleController.rigid.simulated == true)
            {
                if (gameOverRoutine != null)
                {
                    StopCoroutine(gameOverRoutine);
                    gameOverRoutine = null;  // 코루틴을 멈춘 후 null로 초기화
                    spriteRenderer.color = Color.white;
                }
                // 충돌 시간 초기화
                elapsedTime = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            isTouchingBorder = false;
        }
    }

    private IEnumerator ChangeColor(Color targetColor)
    {
        float colorTime = 0;
        while (colorTime < 5f)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, colorTime / 5f);
            colorTime += Time.deltaTime; 
            yield return null; 
        }

        spriteRenderer.color = targetColor;
        DongleEvents.GameOver();
    }
}