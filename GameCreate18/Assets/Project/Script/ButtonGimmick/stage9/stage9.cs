using UnityEngine;

public class stage9 : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;

    [Header("ƒWƒƒƒ“ƒv—Í")]
    [SerializeField] private float normalJump = 5.0f;
    [SerializeField] private float highJump = 8.0f;

    private bool _ButtonState = false;
    void Start()
    {
        if(player != null)
        {
            _ButtonState = player.IsButtonActive;
        }
    }

    void Update()
    {
        if(player == null)
        {
            return;
        }
        if(player.IsButtonActive != _ButtonState )
        {
            if(player.IsButtonActive)
            {
                ToggleJump();
            }
            _ButtonState = player.IsButtonActive;
        }
    }
    private void ToggleJump()
    {
        if(player == null)
        {
            return;
        }
        if(player.jumpForce == normalJump)
        {
            player.jumpForce = highJump;
        }
        else
        {
            player.jumpForce = normalJump;
        }
    }
}
