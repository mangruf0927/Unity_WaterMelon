using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Coroutine blinkCoroutine;
    private float timeElapsed = 0;
    private int dongleCount = 0;
    private bool isBlink = false;

    void Start()
    {
        DongleEvents.OnGameOver += GameOver;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Dongle"))
        {
            dongleCount ++;
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dongle"))
        {
            timeElapsed += Time.deltaTime; // 충돌 시간이 증가

            if(timeElapsed >= 1f && !isBlink)
            {
                isBlink = true;
                blinkCoroutine = StartCoroutine(BlinkEffect(0.5f));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dongle"))
        {
            dongleCount--;

            if (dongleCount <= 0) // 동글이가 모두 떨어졌을 때만 처리
            {
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                }

                isBlink = false;
                spriteRenderer.color = Color.white;
                timeElapsed = 0;
            }
        }
    }
    private IEnumerator BlinkEffect(float time)
    {
        while(true)
        {
            spriteRenderer.color = Color.red; 
            yield return new WaitForSeconds(time); 
            spriteRenderer.color = Color.white; 
            yield return new WaitForSeconds(time);
        } 
    }

    private void GameOver()
    {
        StopCoroutine(blinkCoroutine);
        spriteRenderer.color = Color.white;
    }
}
