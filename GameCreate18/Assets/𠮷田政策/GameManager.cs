using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject mainImage;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject nextButton;

    public GameObject timeBar;
    public GameObject timeText;
    public TimeController timeCnt;

    public GameObject scoreText;
    public static int totalScore = 0;
    public int stageScore = 0;
    public int basePoint = 1000;

    public static float totalGameTime = 0f;
    public static int ResultTime = 0;
    public bool isTimer = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);
        stageScore = 0;
        UpdateScore();
        isTimer = true;
    }

    void Update()
    {
        if (isTimer && PlayerMovement.gameState == "playing")
        {
            totalGameTime += Time.deltaTime;
            if (timeText != null)
            {
                timeText.GetComponent<Text>().text = ((int)totalGameTime).ToString();
            }
        }
        if (PlayerMovement.gameState == "playing")
        {
         GameObject player = GameObject.FindGameObjectWithTag("Player");
          if (player != null)
           {
                PlayerMovement playerCnt = player.GetComponent<PlayerMovement>();

                //if (timeCnt != null && timeText != null)
                //{
                //    int time = (int)timeCnt.displayTime;
                //    timeText.GetComponent<Text>().text = time.ToString();
                //}

                if (playerCnt != null && playerCnt.score != 0)
                {
                    stageScore += playerCnt.score;
                    playerCnt.score = 0;
                    UpdateScore();
                }
            }
        }
    }
    public void ClearGame()
    {
        if(PlayerMovement.gameState == "gameclear")
        {
            isTimer = false;
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

        mainImage.GetComponent<Image>().sprite = gameClearSpr;

        totalScore = basePoint + timeBonus + (stageScore * 300);

            ResultTime = (int)totalGameTime;
        }
    }

    public void NextStage()
    {
        isTimer = false;
        if (PlayerMovement.gameState == "next")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);

            mainImage.GetComponent<Image>().sprite = gameClearSpr;
        }
    }
    public void ResumeGame()
    {
        panel.SetActive(false);
        isTimer = true;
        PlayerMovement.gameState = "playing";
    }

    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.GetComponent<Text>().text = "Items: " + stageScore.ToString();
        }
    }
}