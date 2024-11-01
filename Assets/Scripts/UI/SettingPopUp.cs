using UnityEngine;

public class SettingPopUp : MonoBehaviour
{
    public void OnClickCancle()
    {
        gameObject.SetActive(false);
    }

    public void OnClickContinue()
    {
        gameObject.SetActive(false);
    }

    public void OnClickRestart()
    {
        DongleEvents.Restart();
        gameObject.SetActive(false);
    }
}
