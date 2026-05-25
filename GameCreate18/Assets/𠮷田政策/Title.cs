using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void OnClickTitleButton()
    {
        GameManager.totalGameTime = 0f;
        GameManager.stageScore = 0;
        GameManager.totalScore = 0;
        PlayerMovement.gameState = "playing";
    }
}