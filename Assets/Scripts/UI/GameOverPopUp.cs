using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    [SerializeField]    private Camera mainCamera;

    private void OnEnable() 
    {
        mainCamera.orthographicSize *= 1.5f;
        Time.timeScale = 0;
    }

    private void OnDisable() 
    {
        mainCamera.orthographicSize /= 1.5f;
        Time.timeScale = 1;
    }

    public void OnClickRestart()
    {
        SoundManager.Instance.PlaySFX("Button");

        DongleEvents.Restart();
        gameObject.SetActive(false);
    }
}
