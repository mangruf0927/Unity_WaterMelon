using UnityEngine;

public class ExitMessage : MonoBehaviour
{
    public void OnClickYes()
    {
        Application.Quit();
    }

    public void OnClickNo()
    {
        gameObject.SetActive(false);
    } 
}
