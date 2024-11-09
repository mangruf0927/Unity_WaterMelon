using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public delegate void InputBoolHandler(bool isTouch);
    public event InputBoolHandler OnTouchStart;
    public event InputBoolHandler OnTouchEnd;

    public delegate void InputVectorHandler(Vector2 position);
    public event InputVectorHandler OnTouchStay;

    private bool isTouchUI = false;

    void Update()
    {
        if (Input.touchCount > 0)  // 터치가 하나 이상 있는지 확인
        {
            Touch touch = Input.GetTouch(0);  // 첫 번째 터치 가져오기            

            // 터치 시작 상태일 때
            if (touch.phase == TouchPhase.Began)
            {
                isTouchUI = EventSystem.current.IsPointerOverGameObject();  // 터치가 UI 위에서 시작되었는지 기록
                if (isTouchUI) return;  // UI 터치 시 이벤트 중지

                OnTouchStart?.Invoke(true);  
            }
            // 터치가 유지되거나 움직였을 때
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (isTouchUI) return;

                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                OnTouchStay?.Invoke(touchPosition);
            }
            // 터치가 끝났을 때
            else if (touch.phase == TouchPhase.Ended)
            {
                if (isTouchUI) 
                {
                    isTouchUI = false;  // 상태 초기화
                    return;  // UI 터치가 끝났을 때 이벤트 중지
                }   

                OnTouchEnd?.Invoke(false);  
            }
        }

        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKey(KeyCode.Escape))
            {   
                DongleEvents.ExitGame();
            }
        }   
    }
}
