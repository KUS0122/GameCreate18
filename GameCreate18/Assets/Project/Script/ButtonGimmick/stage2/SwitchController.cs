using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField]
    private MovingFloor[] movingFloors;

    private bool isPressed = false;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player =
            other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        PressSwitch();
    }

    private void PressSwitch()
    {
        if (isPressed)
            return;

        isPressed = true;

        foreach (MovingFloor floor in movingFloors)
        {
            if (floor == null)
                continue;

            floor.Activate();
        }

    }

    public void ResetSwitch()
    {
        isPressed = false;

        foreach (MovingFloor floor in movingFloors)
        {
            if (floor == null)
                continue;

            floor.ResetFloor();
        }
    }
}