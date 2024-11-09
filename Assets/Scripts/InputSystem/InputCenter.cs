using Unity.VisualScripting;
using UnityEngine;

public class InputCenter : MonoBehaviour
{
    [SerializeField]    private InputHandler inputHandler;
    [SerializeField]    private DongleCenter dongleCenter;

    [SerializeField]    private DongleController dongleController;

    private void Awake() 
    {
        dongleCenter.OnGetController += SetDongleController;
    }

    private void SetDongleController(DongleController controller)
    {
        dongleController = controller;

        inputHandler.OnTouchStart += StartTouch;
        inputHandler.OnTouchStay += StayTouch;
        inputHandler.OnTouchEnd += EndTouch;
    }

    private void StartTouch(bool isTouch)
    {
        if(dongleController != null)
            dongleController.TouchDongle(isTouch);
    }

    private void StayTouch(Vector2 position)
    {
        if(dongleController != null)
        {
            float dongleX = Mathf.Clamp(position.x, GameConstants.LEFT_BOUNDARY + dongleController.sprite.bounds.size.x / 2f, GameConstants.RIGHT_BOUNDARY - dongleController.sprite.bounds.size.x / 2f);
            Vector2 donglePosition = new Vector2(dongleX, GameConstants.DONGLE_HEIGHT);
            
            dongleController.SetDonglePosition(donglePosition);        }
    }

    private void EndTouch(bool isTouch)
    {
        if(dongleController != null)
        {
            dongleController.DropDongle(isTouch);
            dongleCenter.DropDongle();
            dongleController = null;
        }
    }
}
