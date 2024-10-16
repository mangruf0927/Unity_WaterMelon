using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DongleHitScan : MonoBehaviour
{
    [Header("동글이 컨트롤러")]
    [SerializeField] DongleController dongleController;

    [Header("동글이 스프라이트")]
    [SerializeField] SpriteRenderer spriteRenderer;

    private GameCenter gameCenter;

    public void SetGameCenter(GameCenter center)
    {
        gameCenter = center;
    }

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
        DongleController newDongle = DongleFactory.CreateDongle(gameObject ,dongleController.dongleLevel + 1, gameCenter);
        newDongle.transform.position = collisionPoint; // 충돌 지점에 생성
        newDongle.rigid.simulated = true;
    }

    private void AddScore(int level)
    {
        int score = (level * (level + 1)) / 2;
        gameCenter.AddScore(score);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            spriteRenderer.color = Color.white;
        }    
    }

    // 동글 - Border 충돌
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            if(transform.position.y > other.transform.position.y)
            {
                StartCoroutine(ChangeColor(Color.red)); 
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }    
    }

    private IEnumerator ChangeColor(Color targetColor)
    {
        float timeElapsed = 0;
        while (timeElapsed < 1.0f)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, timeElapsed);
            timeElapsed += Time.deltaTime * 1.5f; 
            yield return null; 
        }

        spriteRenderer.color = targetColor;

        yield return new WaitForSeconds(3.0f); 
        gameCenter.GameOver();
    }
}