using UnityEngine;

public class GuideLine : MonoBehaviour, IObserver
{
    [SerializeField]    private LineRenderer lineRenderer;

    private void Awake()
    {
        // 선 굵기
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        // 점 개수
        lineRenderer.positionCount = 2;
    }
    
    public void EnableLine(bool isEnabled)
    {
        lineRenderer.enabled = isEnabled;
    }

    public void DrawLine(Vector2 startPosition, Vector2 endPosition)
    {
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    public void Notify(ISubject subject)
    {
        if(subject is DongleController dongle)
        {
            DrawLine(dongle.transform.position, new Vector2(dongle.transform.position.x, -3.5f));
        }
    }
}
