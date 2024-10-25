using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private float referenceWidth = 1080f; // 기준 가로 해상도
    private float referenceHeight = 1920f; // 기준 세로 해상도
    private float referenceOrthographicSize = 8f; // 기준 카메라 사이즈

    private void Start()
    {
        // 현재 화면의 가로 세로 비율 가져오기
        float currentAspect = (float)Screen.width / Screen.height;
        float referenceAspect = referenceWidth / referenceHeight;

        // 기준 orthographicSize에 가로 비율을 곱하여 카메라 사이즈 조정
        mainCamera.orthographicSize = referenceOrthographicSize * (referenceAspect / currentAspect);
    }
}