using UnityEngine;

public class InGameButton : MonoBehaviour
{
    [Header("Setting 팝업")]
    [SerializeField]    private GameObject settingPopUp; 

    [Header("Rank 팝업")]
    [SerializeField]    private GameObject rankPopUp;

    public void OnClickSetting()
    {
        settingPopUp.SetActive(true);
    }

    public void OnClickRank()
    {
        rankPopUp.SetActive(true);
    }
}
