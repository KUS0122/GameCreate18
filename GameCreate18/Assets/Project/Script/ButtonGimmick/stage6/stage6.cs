using UnityEngine;

public class stage6 : StageBase
{
    [SerializeField] private PlayerMovement player;

    private bool _ButtonState = false;
    private bool trigger = false;

    void Start()
    {
       ResetStage();
    }

    void Update()
    {
        if(player == null)
        {
            return;
        }
        if(trigger)
        {
            return;
        }

        if(player.IsButtonActive != _ButtonState)
        {
            if(player.IsButtonActive)
            {
                TogglPlayerControl();
                trigger = true;
            }
            _ButtonState = player.IsButtonActive;
        }
    }
    private void TogglPlayerControl()
    {
        player.Isinvert = !player.Isinvert;
    }
    public override void ResetStage()
    {
        trigger = false;
        if (player != null)
        {
            _ButtonState = player.IsButtonActive;
        }
    }
    public override void StopStage()
    {

    }
}
