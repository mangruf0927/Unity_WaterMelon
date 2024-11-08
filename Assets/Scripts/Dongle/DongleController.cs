using System.Collections.Generic;
using UnityEngine;

public class DongleController : MonoBehaviour, ISubject
{
    [SerializeField]    public Rigidbody2D rigid;
    [SerializeField]    private CircleCollider2D dongleCollider;
    [SerializeField]    private Animator animator;
    [SerializeField]    public SpriteRenderer sprite;
    
    public int dongleLevel;
    public int dongleMaxLevel;
    public bool isMerge = false;
    
    private bool isTouch = false;

    public List<IObserver> lineObserverList = new List<IObserver>();


    private void OnEnable() 
    {
        isMerge = false;
        sprite.color = Color.white;

        NotifyObservers();
    }

    public void PlayAnimation(int level)
    {
        animator.Play("Level" + level);
    }

    public void SetDonglePosition(Vector2 pos)
    {
        if(isTouch)
        {
            transform.position = Vector2.Lerp(transform.position, pos, 0.2f);
        }
        else
        {
            transform.position = pos;
        }

        NotifyObservers();
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

    // >> 
    public void AddObserver(IObserver observer)
    {
        if (!lineObserverList.Contains(observer))
        {
            lineObserverList.Add(observer);
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        if (lineObserverList.Contains(observer))
        {
            lineObserverList.Remove(observer);
        }
    }

    public void NotifyObservers()
    {
        foreach (IObserver observer in lineObserverList)
        {
            observer.Notify(this);
        }
    }
}

