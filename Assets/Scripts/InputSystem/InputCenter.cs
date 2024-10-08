using Unity.VisualScripting;
using UnityEngine;

public class InputCenter : MonoBehaviour
{
    public InputHandler inputHandler;
    public DongleCenter dongleCenter;

    private DongleController dongleController;

    private void Start() 
    {
        dongleCenter.OnGetController += SetDongleController;

        if(dongleController != null)
        {
            inputHandler.OnTouchStart += StartTouch;
            inputHandler.OnTouchStay += StayTouch;
            inputHandler.OnTouchEnd += EndTouch;
        }
    }

    private void SetDongleController(DongleController controller)
    {
        dongleController = controller;
    }

    private void StartTouch(bool isTouch)
    {
        dongleController.TouchDongle(isTouch);
    }

    private void StayTouch(Vector2 position)
    {
        dongleController.SetDonglePosition(position);
    }

    private void EndTouch(bool isTouch)
    {
        dongleController.DropDongle(isTouch);
        dongleCenter.DropDongle();
    }
}
