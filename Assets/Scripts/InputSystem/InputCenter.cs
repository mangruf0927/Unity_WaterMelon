using UnityEngine;

public class InputCenter : MonoBehaviour
{
    public InputHandler inputHandler;

    private void Start() 
    {
        inputHandler.OnTouchStart += StartTouch;
        inputHandler.OnTouchStay += StayTouch;
        inputHandler.OnTouchEnd += EndTouch;
    }

    private void StartTouch(bool isTouch)
    {
        Debug.Log("화면 터치 시작 " + isTouch);
    }

    private void StayTouch(Vector2 position)
    {
        Debug.Log("화면 " + position + " 터치 중");
    }

    private void EndTouch(bool isTouch)
    {
        Debug.Log("화면 터치 끝 " + isTouch);
    }
}
