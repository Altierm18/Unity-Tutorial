using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Clock : MonoBehaviour
{
    [SerializeField]
    Transform hoursPivot, minutePivot, secondPivot;

    private void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hoursPivot.localRotation = Quaternion.Euler(0f, 0f, -30f * (float)time.TotalHours); //30 as 360/12 = 30
        minutePivot.localRotation = Quaternion.Euler(0f, 0f, -6f * (float)time.TotalMinutes); //6 as 360 / 60 = 6
        secondPivot.localRotation = Quaternion.Euler(0f, 0f, -6f * (float)time.TotalSeconds);
    }
}
