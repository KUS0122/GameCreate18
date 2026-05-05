using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using UnityEditor.Timeline.Actions;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject mainImage;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;
    Image titleImage;

    void Start()
    {
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);
    }
    void Update()
    {
        if (PlayerController.gameState == "gameclear")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "gameend";
        }
        else if (PlayerController.gameState == "gameover")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";
        }
        else if (PlayerController.gameState == "playing")
        {

        }
    }
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
}
