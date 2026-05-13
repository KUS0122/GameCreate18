using UnityEngine;
using UnityEngine.UI;

public class GoalChange : MonoBehaviour
{
    public GameObject exitButton;
    public GameObject mainImage;
    public GameObject panel;
    public GameObject playerObject;

    public void ResumeGame()
    {
        if (playerObject != null)
        {
            Rigidbody rb = playerObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            playerObject.transform.position -= playerObject.transform.forward * 2.0f;
        }
        panel.SetActive(false);
        mainImage.SetActive(false);
        PlayerController.gameState = "playing";
    }
}
