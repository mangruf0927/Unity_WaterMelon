using UnityEngine;

public class RankPopUp : MonoBehaviour
{
    public void OnClickCancle()
    {
        SoundManager.Instance.PlaySFX("Button");
        
        gameObject.SetActive(false);
    }
}
