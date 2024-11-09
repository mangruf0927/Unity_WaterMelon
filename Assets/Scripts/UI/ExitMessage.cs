using UnityEngine;

public class ExitMessage : MonoBehaviour
{
    public void OnClickYes()
    {
        SoundManager.Instance.PlaySFX("Button");

        Application.Quit();
    }

    public void OnClickNo()
    {
        SoundManager.Instance.PlaySFX("Button");

        gameObject.SetActive(false);
    } 
}
