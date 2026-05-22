using UnityEngine;

public class Stage7 : MonoBehaviour
{
    [SerializeField] private AutoDoorStage7[] autoDoors;

    [SerializeField] private int needSwitchCount = 7;

    private int pressedCount = 0;
    private bool doorUnlocked = false;

    private void Start()
    {
        foreach (AutoDoorStage7 door in autoDoors)
        {
            door.SetCanOpen(false);
        }
    }

    public void OnSwitchPressed()
    {
        if (doorUnlocked)
            return;

        pressedCount++;

        //Debug.Log("Switch Count : " + pressedCount);

        if (pressedCount >= needSwitchCount)
        {
            doorUnlocked = true;

            foreach (AutoDoorStage7 door in autoDoors)
            {
                door.SetCanOpen(true);
            }

            //Debug.Log("AutoDoor Unlock");
        }
    }
}