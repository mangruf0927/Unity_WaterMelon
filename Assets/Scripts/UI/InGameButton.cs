using UnityEngine;

public class InGameButton : MonoBehaviour
{
    [Header("Setting 팝업")]
    [SerializeField]    private GameObject settingPopUp; 

    [Header("Rank 팝업")]
    [SerializeField]    private GameObject rankPopUp;

    public void OnClickSetting()
    {
        SoundManager.Instance.PlaySFX("Button");

        settingPopUp.SetActive(true);
    }

    public void OnClickRank()
    {
        SoundManager.Instance.PlaySFX("Button");
        
        rankPopUp.SetActive(true);
    }
}
