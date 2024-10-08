using UnityEngine;

public class InputCenter : MonoBehaviour
{
    public InputHandler inputHandler;
    private DongleController dongleController;

    public void SetDongleController(DongleController controller)
    {
        dongleController = controller;
    }

    private void Start() 
    {
        inputHandler.OnTouchStart += StartTouch;
        inputHandler.OnTouchStay += StayTouch;
        inputHandler.OnTouchEnd += EndTouch;
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
    }
}
