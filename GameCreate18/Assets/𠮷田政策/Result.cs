using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Result : MonoBehaviour
{
    public Text resultScoreText;
    public Text timeText;
    void Start()
    {
        resultScoreText.GetComponent<Text>().text = GameManager.totalScore.ToString();
        timeText.GetComponent<Text>().text = ((int)TimeController.gameTime).ToString();
    }
}
