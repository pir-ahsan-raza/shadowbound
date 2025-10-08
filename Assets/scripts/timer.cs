using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text timerText; // Drag your UI Text object here in the Inspector
    private float timeElapsed = 0f;
    private bool timerRunning = true;

    void Update()
    {
        if (timerRunning)
        {
            timeElapsed += Time.deltaTime;

            // Stop after 3 minutes (180 seconds)
            if (timeElapsed >= 180f)
            {
                timeElapsed = 180f;
                timerRunning = false;
            }

            // Convert to minutes:seconds format
            int minutes = Mathf.FloorToInt(timeElapsed / 60);
            int seconds = Mathf.FloorToInt(timeElapsed % 60);

            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }
}