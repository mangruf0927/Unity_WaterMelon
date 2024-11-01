using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    [SerializeField]    private Camera mainCamera;

    private void OnEnable() 
    {
        mainCamera.orthographicSize *= 1.5f;
    }

    private void OnDisable() 
    {
        mainCamera.orthographicSize /= 1.5f;
    }

    public void OnClickRestart()
    {
        DongleEvents.Restart();
        gameObject.SetActive(false);
    }
}
