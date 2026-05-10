using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    public GameObject timeBar;
    public GameObject timeText;
    public TimeController timeCnt;

    public GameObject scoreText;
    public static int totalScore = 0;
    public int stageScore = 0;
    public int basePoint = 1000;

    void Start()
    {
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);
        stageScore = 0;
        UpdateScore();
    }
    void Update()
    {
        if (PlayerController.gameState == "gameclear")
        {
            int timeBonus = 0;
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;
                float baseTime = 300f;
                float remain = Mathf.Max(0, baseTime - TimeController.gameTime);
                timeBonus = (int)(remain * 50f);
            }

            mainImage.SetActive(true);
            panel.SetActive(true);
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "gameend";
            totalScore = basePoint + timeBonus + stageScore * 300;
            UpdateScore();
        }
        else if (PlayerController.gameState == "gameover")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;
            }
        }
        else if (PlayerController.gameState == "playing")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerController playerCnt = player.GetComponent<PlayerController>();
                if (timeCnt != null && timeText != null)
                {
                    int time = (int)timeCnt.displayTime;
                    timeText.GetComponent<Text>().text = time.ToString();
                }

                if (playerCnt.score != 0)
                {
                    stageScore += playerCnt.score;
                    playerCnt.score = 0;
                    UpdateScore();
                }
            }
        }
    }
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }
    void UpdateScore()
    {
        scoreText.GetComponent<Text>().text = "Items: " + stageScore.ToString();
    }
}
