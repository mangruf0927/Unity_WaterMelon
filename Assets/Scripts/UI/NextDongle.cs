using UnityEngine;
using UnityEngine.UI;

public class NextDongle : MonoBehaviour, IObserver
{
    public Image image;
    public Sprite[] nextDongleImage;

    public void Notify(ISubject subject)
    {
        if(subject is DongleCenter dongle)
        {
            UpdateNextDongleUI(dongle);
        }
    }

    public void UpdateNextDongleUI(DongleCenter dongle)
    {
        image.sprite = nextDongleImage[dongle.GetNextLevel() - 1];
    }
}
