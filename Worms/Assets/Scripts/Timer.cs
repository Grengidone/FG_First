using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timerLocation;
    public float timerRatio;
    private bool startTimer = false;

    private float timePast = 0f;
    private float timerDestination = 0f;

    public Timer()
    {
    }

    public void StartTimer(float time)
    {
        timePast = 0;
        startTimer = true;
        timerDestination = time;

    }

    public void StartTimer (float time, float currentTime)
    {
        startTimer = true;
        timerDestination = time;
        timePast = currentTime;
    }

    private void Update()
    {
        if (startTimer)
        {
            timePast += Time.deltaTime;
            if (timerDestination <= timePast)
            {
                startTimer = false;

            }
        }
    }

    public float GetTimerRatio()
    {
        if (startTimer)
        {
            return Mathf.Clamp (1 - (timePast / timerDestination), 0, 1);
        }
        else
        {
            return 0;
        }
    }

    public float GetTimer()
    {
        return timePast;
    }
}
