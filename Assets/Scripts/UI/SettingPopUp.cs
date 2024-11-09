using UnityEngine;
using UnityEngine.UI;

public class SettingPopUp : MonoBehaviour
{
    [SerializeField]    private Toggle sfxToggle;
    [SerializeField]    private Toggle bgmToggle;

    private void Start()
    {
        sfxToggle.onValueChanged.AddListener(OnOffSFX);
        bgmToggle.onValueChanged.AddListener(OnOffBGM);
    }

    public void OnClickCancle()
    {
        SoundManager.Instance.PlaySFX("Button");

        gameObject.SetActive(false);
    }

    public void OnClickContinue()
    {
        SoundManager.Instance.PlaySFX("Button");

        gameObject.SetActive(false);
    }

    public void OnClickRestart()
    {
        SoundManager.Instance.PlaySFX("Button");
        
        DongleEvents.Restart();
        gameObject.SetActive(false);
    }

    public void OnOffBGM(bool isOn)
    {
        SoundManager.Instance.PlaySFX("Button");
        
        if(!isOn)
        {
            SoundManager.Instance.ChangeBGMVolume(0.3f);
        }
        else
        {
            SoundManager.Instance.ChangeBGMVolume(0);
        }

    }

    public void OnOffSFX(bool isOn)
    {
        SoundManager.Instance.PlaySFX("Button");

        if(!isOn)
        {
            SoundManager.Instance.ChangeSFXVolume(1);
        }
        else
        {
            SoundManager.Instance.ChangeSFXVolume(0);
        }
    }
}
