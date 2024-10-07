using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void InputBoolHandler(bool isTouch);
    public event InputBoolHandler OnTouchStart;
    public event InputBoolHandler OnTouchEnd;

    public delegate void InputVectorHandler(Vector2 position);
    public event InputVectorHandler OnTouchStay;

    void Update()
    {
        if (Input.touchCount > 0)  // 터치가 하나 이상 있는지 확인
        {
            Touch touch = Input.GetTouch(0);  // 첫 번째 터치 가져오기

            // 터치 시작 상태일 때
            if (touch.phase == TouchPhase.Began)
            {
                OnTouchStart?.Invoke(true);  
            }
            // 터치가 유지되거나 움직였을 때
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                OnTouchStay?.Invoke(touchPosition);
            }
            // 터치가 끝났을 때
            if (touch.phase == TouchPhase.Ended)
            {
                OnTouchEnd?.Invoke(false);  
            }
        }
        
        #if UNITY_EDITOR        
        // 마우스 클릭 시작 시
        if (Input.GetMouseButtonDown(0))
        {
            OnTouchStart?.Invoke(true);  // 마우스 클릭 시작 시 isTouch = true 전달
        }
        // 마우스 클릭 유지 중일 때
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            OnTouchStay?.Invoke(mousePosition);
        }
        // 마우스 클릭 끝났을 때
        if (Input.GetMouseButtonUp(0))
        {
            OnTouchEnd?.Invoke(false);  // 마우스 클릭 끝날 때 isTouch = false 전달
        }
        #endif
    }
}
