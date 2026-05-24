using UnityEngine;

public class stage9 : StageBase
{
    [SerializeField] private PlayerMovement player;

    [Header("ƒWƒƒƒ“ƒv—Í")]
    [SerializeField] private float normalJump = 5.0f;
    [SerializeField] private float highJump = 8.0f;
    [SerializeField] public float Duration = 10.0f;
    [SerializeField] private float Timer = 0.0f;

   // private bool _ButtonState = false;
    private bool _isHighJumpActive = false;
    private int _PressCount = 0;
    void Start()
    {
        if(player != null)
        {
            //_ButtonState = player.IsButtonActive;
            _PressCount = player.buttonPressCount;
        }
    }

    void Update()
    {
        if(player == null)
        {
            return;
        }
        if(player.buttonPressCount != _PressCount)
        {
            HighJump();
        }
        _PressCount = player.buttonPressCount;
        if(_isHighJumpActive)
        {
            Timer -= Time.deltaTime;

            if( Timer <= 0.0f )
            {
                ResetJump();
            }
            else
            { 
            player.jumpForce = highJump;
            }
        }
    }
    private void HighJump()
    {
        Timer = Duration;
        _isHighJumpActive = true;
        if(player != null)
        {
            player.jumpForce = highJump;
        }
    }
    private void ResetJump()
    {
        player.jumpForce = normalJump;
        Timer = 0.0f;
        _isHighJumpActive = false;
    }
    public override void StopStage()
    {
        if (player != null) ResetJump();
    }
    public override void ResetStage()
    {
        if (player != null)
        {
            ResetJump();
            _PressCount = player.buttonPressCount;
        }
    }

    /*public override void ResetStage()
    {
        player.jumpForce = normalJump;
    }*/
}
