using UnityEngine;

public class DongleController : MonoBehaviour
{
    [SerializeField]    private Rigidbody2D rigid;
    [SerializeField]    private CircleCollider2D dongleCollider;
    [SerializeField]    private Animator animator;

    public int dongleLevel;
    
    private Vector2 donglePosition;
    private bool isTouch;
    private bool isDrop = false;

    private void Start() 
    {
        rigid = GetComponent<Rigidbody2D>();
        dongleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();    
    }

    private void OnEnable() 
    {
        PlayAnimation(dongleLevel);
    }

    private void PlayAnimation(int level)
    {
        animator.Play("level" + level);
    }

    public void SetDonglePosition(Vector2 pos)
    {
        if(isTouch)
        {
            donglePosition.x = Mathf.Clamp(pos.x, -4.5f + transform.localScale.x / 2f, 4.5f - transform.localScale.x / 2f);
            donglePosition.y = 6.5f;

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
        isDrop = !isTrue;
        rigid.simulated = !isTrue; // 모든 물리 연산 중지
    }

    public bool CheckDongleDropped()
    {
        return isDrop;
    }
}

