using UnityEngine;

public class stage6 : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;

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
        if(player.IsButtonActive != _ButtonState)
        {
            if(player.IsButtonActive)
            {
                TogglPlayerControl();
            }
            _ButtonState = player.IsButtonActive;
        }
    }
    private void TogglPlayerControl()
    {
        player.Isinvert = !player.Isinvert;
    }
}
