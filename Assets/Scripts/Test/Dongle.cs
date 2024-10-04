using System;
using System.Collections;
using UnityEngine;

public class Dongle : MonoBehaviour
{
    public int level;
    public bool isDrag;
    public bool isMerge;
    
    public Rigidbody2D rigid;
    public CircleCollider2D col;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public GameManager gameManager;
    public ParticleSystem effect;

    float deadTime;

    private void OnEnable() 
    {
        animator.Play("Level" + level);
    }

    void Update()
    {
        if(isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 스크린 좌표 -> World 좌표
            float leftBorder = -4.2f + transform.localScale.x / 2f;
            float rightBorder = 4.2f - transform.localScale.x / 2f;

            // x 축 경계 설정
            if(mousePos.x < leftBorder)
                mousePos.x = leftBorder;
            else if(mousePos.x > rightBorder)
                mousePos.x = rightBorder;

            mousePos.y = 8;
            mousePos.z = 0;
            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);
        }
    }

    public void Drag()
    {
        isDrag = true;
    }

    public void Drop()
    {
        isDrag = false;
        rigid.simulated = true;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Dongle")
        {
            Dongle other = collision.gameObject.GetComponent<Dongle>();

            if(level == other.level && !isMerge && !other.isMerge && level < 4)
            {

                float meX = transform.position.x;
                float meY = transform.position.y;

                float otherX = other.transform.position.x;
                float otherY = other. transform.position.y;

                if(meY < otherY || (meY == otherY && meX > otherX))
                {
                    // 상대방 숨기기
                    other.Hide(transform.position);

                    // 나는 레벨 업
                    LevelUp();
                }
            }
        }
    }

    public void Hide(Vector3 targetPos)
    {
        isMerge = true;

        rigid.simulated = false;
        col.enabled = false;

        if(targetPos == Vector3.up * 100)
        {
            PlayEffect();
        }

        StartCoroutine(HideRoutine(targetPos));
    }

    IEnumerator HideRoutine(Vector3 targetPos)
    {
        int frameCount = 0;

        while(frameCount < 20)
        {
            frameCount++;

            if(targetPos != Vector3.up * 100)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
            }
            else if(targetPos == Vector3.up * 100)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.2f);
            }

            yield return null;
        }
        gameManager.score += (int)Mathf.Pow(2, level);

        isMerge = false;
        gameObject.SetActive(false);
    }

    private void LevelUp()
    {
        isMerge = true;

        rigid.linearVelocity = Vector2.zero;
        rigid.angularVelocity = 0; // 회전 속도

        StartCoroutine(LevelUpRoutine());
    }

    private IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        
        animator.Play("Level" + (level + 1));
        PlayEffect();

        yield return new WaitForSeconds(0.2f);
        level ++;

        gameManager.maxLevel = Mathf.Max(level, gameManager.maxLevel);

        isMerge = false;
    }

    private void PlayEffect()
    {
        effect.transform.position = transform.position;
        effect.transform.localScale = transform.localScale;
        effect.Play();
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag == "Line")
        {
            deadTime += Time.deltaTime;

            if(deadTime > 2)
            {
                spriteRenderer.color = Color.red; // new Color(r, g, b);
            }
            if(deadTime > 5)
            {
                gameManager.GameOver();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Line")
        {
            deadTime = 0;
            spriteRenderer.color = new Color(79f / 255f, 78f / 255f, 58f / 255f);;
        }    
    }
}
