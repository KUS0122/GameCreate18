using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class TimeController : MonoBehaviour
{
    public bool isCountDown = false;
    public bool isTimeOver = false;
    float times = 0;


    public static float gameTime
    {
        get
        {
            if(GameManager.instance != null)
            {
                return GameManager.totalGameTime;
            }
            return 0;
        }
    }
    public float displayTime => gameTime;
    void Start()
    {
        isTimeOver = false;
    }
    void Update()
    {
        if (isTimeOver) return;
    }
    public void ResetTime()
    {
        isTimeOver = false;
    }
}
