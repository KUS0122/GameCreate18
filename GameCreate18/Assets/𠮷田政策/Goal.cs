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
    public Text errorText;

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
            if(errorText != null)
            {
                errorText.text = "";
            }
            if (exitButton != null)
            {
            exitButton.SetActive(true);
            Button bt = exitButton.GetComponent<Button>();
                if (bt != null)
                {
                    bt.interactable = true;
                }
            }
            PlayerMovement.gameState = "gameend";
        }
    }
    public void InputPass()
    {
        if (inputField.text == "1234")
        {
            panel.SetActive(false);
            PlayerMovement.gameState = "gameclear";
            if (GameManager.instance != null)
            {
                GameManager.instance.ClearGame();
            }
        }
        else
        {
            panel.SetActive(true);
            inputField.text = "";
            inputField.ActivateInputField();
            if(errorText != null)
            {
                errorText.text = "パスワードが違います";
            }
        }
    }
}
