using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class TimeController : MonoBehaviour
{
    public bool isCountDown = false;
    public static float gameTime = 0;
    public bool isTimeOver = false;
    float times = 0;

    public float displayTime => gameTime;
    void Start()
    {
        gameTime = 0;
        isTimeOver = false;
    }
    void Update()
    {
        if (isTimeOver) return;
        gameTime += Time.deltaTime;
    }
    public void ResetTime()
    {
        gameTime = 0;
        isTimeOver = false;
        Debug.Log("TIMES:" + displayTime);
    }
}
