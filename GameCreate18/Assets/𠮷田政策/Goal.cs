using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject exitButton;
    public GameObject mainImage;
    public GameObject panel;

    public InputField inputField;
    public GameObject fieldObject;

    void Start()
    {
        panel.SetActive(false);
    }
    void Update()
    {
        if (PlayerMovement.gameState == "gameGoal")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            exitButton.SetActive(true);
            Button bt = exitButton.GetComponent<Button>();
            bt.interactable = true;
            PlayerMovement.gameState = "gameend";
        }
    }
    public void InputPass()
    {
        if (inputField.text == "1234")
        {
            panel.SetActive(false);
            PlayerMovement.gameState = "gameclear";
        }
    }
}
