using UnityEngine;

public class DongleController : MonoBehaviour
{
    [SerializeField]    public Rigidbody2D rigid;
    [SerializeField]    private CircleCollider2D dongleCollider;
    [SerializeField]    private Animator animator;

    public int dongleLevel;
    public int dongleMaxLevel;
    public bool isMerge = false;
    
    private Vector2 donglePosition;
    private bool isTouch = false;

    private void OnEnable() 
    {
        isMerge = false;
    }

    public void PlayAnimation(int level)
    {
        animator.Play("Level" + level);
    }

    public void SetDonglePosition(Vector2 pos)
    {
        if(isTouch)
        {
            donglePosition.x = Mathf.Clamp(pos.x, -3.5f + transform.localScale.x / 2f, 3.5f - transform.localScale.x / 2f);
            donglePosition.y = 5f;

            transform.position = Vector2.Lerp(transform.position, donglePosition, 0.2f);
        }
    }

    public void TouchDongle(bool isTrue)
    {
        isTouch = isTrue;
    }

    public void DropDongle(bool isTrue)
    {
        isTouch = isTrue;
        rigid.simulated = !isTrue; // 모든 물리 연산 중지
    }
}

