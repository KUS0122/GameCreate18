using UnityEngine;

public class ButtonController : MonoBehaviour
{
    bool isActive = true;

    public void OnPressed(PlayerController1 player)
    {
        if (!isActive) return;

        isActive = false;

        // Œ©‚½–Ú‚¾‚¯Á‚·
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        player.SetButtonActive(true);

    }

    public void ResetButton()
    {
        isActive = true;
        GetComponent<Collider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }
}