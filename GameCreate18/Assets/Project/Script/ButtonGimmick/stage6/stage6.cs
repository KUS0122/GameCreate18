using UnityEngine;
using UnityEngine.InputSystem;

public class stage6 : StageBase
{
    [SerializeField] private PlayerMovement player;

    private int _ButtonState = 0;
    private bool trigger = false;
    private bool _playerState = false;

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

        if(player.buttonPressCount > _ButtonState)
        {
            TogglPlayerControl();
            _ButtonState = player.buttonPressCount;
        }
    }
    private void TogglPlayerControl()
    {
        if (player == null)
        {
            return;
        }
        _playerState = !_playerState;
        player.Isinvert = _playerState;
    }
    public override void ResetStage()
    {
        trigger = false;
        _playerState = false;
        if (player != null)
        {
            _ButtonState = player.buttonPressCount;
        }
    }
    public override void StopStage()
    {

    }
}
