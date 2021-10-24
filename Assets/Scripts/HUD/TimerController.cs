using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public Text timeCounter;

    public float totalTime;

    private float minutes;
    private float seconds;

    private bool timerGoing;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Debug.Log("Start");
        timeCounter.text = "00:00";
        timerGoing = false;
    }

    public void BeginTimer()
    {
        timerGoing = true;
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    void Update()
    {
        totalTime -= Time.deltaTime;
        minutes = (int)(totalTime / 60);
        seconds = (int)(totalTime % 60);

        timeCounter.text = minutes.ToString() + ":" + seconds.ToString();
    }
}
