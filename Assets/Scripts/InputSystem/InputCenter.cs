using Unity.VisualScripting;
using UnityEngine;

public class InputCenter : MonoBehaviour
{
    public InputHandler inputHandler;
    public DongleCenter dongleCenter;

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
            dongleController.SetDonglePosition(new Vector2(position.x, GameConstants.DONGLE_HEIGHT));
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
