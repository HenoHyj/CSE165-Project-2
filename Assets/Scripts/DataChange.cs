using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataChange : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textForCheck;
    [SerializeField] private TextMeshProUGUI textForTime;
    [SerializeField] private TextMeshProUGUI textForDistance;
    private float startTime;
    private float finalTime;
    private float tempRecord = -1.0f;
    // Start is called before the first frame update
    void Start()
    {
        textForCheck.enabled = false;
        textForTime.text = "Time Spent (in second): 0";
        textForDistance.text = "Distance: 0";
    }

    // Update is called once per frame
    void Update()
    {
        // Check errors
        if (GlobalVariables.errCheck)
        {
            textForCheck.enabled = true;
            textForCheck.text = "Check for random errors!";
            return;
        }

        // Run if no error
        if (GlobalVariables.starting)
        {
            if (GlobalVariables.isMoving)
            {
                if (tempRecord < 0)
                {
                    tempRecord = Time.time;
                }
                textForCheck.enabled = true;
                int countDown = 3 - Mathf.FloorToInt(Time.time - tempRecord);
                textForCheck.text = "Get Ready in " + countDown.ToString();
                // Update corresponding variables when countdown ends
                if (countDown <= 0)
                {
                    GlobalVariables.starting = false;
                    GlobalVariables.isGaming = true;
                    tempRecord = -1.0f;
                    textForCheck.enabled = false;
                    startTime = Time.time;
                }
            }
            else
            {
                textForCheck.enabled = true;
                textForCheck.text = "Get Ready for the test!";
                tempRecord = -1.0f;
            }
        }

        // Gaming part
        else if (GlobalVariables.isGaming)
        {
            if (GlobalVariables.crashed)
            {
                if (tempRecord < 0)
                {
                    tempRecord = Time.time;
                }
                textForCheck.enabled = true;
                int countDown = 3 - Mathf.FloorToInt(Time.time - tempRecord);
                textForCheck.text = "Crashed! Recover in " + countDown.ToString();
                // Update corresponding variables when countdown ends
                if (countDown <= 0)
                {
                    GlobalVariables.crashed = false;
                    GlobalVariables.isMoving = false; // Insurance (though may not really work)
                    tempRecord = -1.0f;
                    textForCheck.enabled = false;
                }
            }
            else
            {
                if (GlobalVariables.reachCheck)
                {
                    if (tempRecord < 0)
                    {
                        tempRecord = Time.time;
                    }
                    textForCheck.enabled = true;
                    int countDown = 1 - Mathf.FloorToInt(Time.time - tempRecord);
                    textForCheck.text = "Check Point Reached!";
                    // Update corresponding variables when countdown ends
                    if (countDown <= 0)
                    {
                        GlobalVariables.reachCheck = false;
                        tempRecord = -1.0f;
                        textForCheck.enabled = false;
                    }
                }
            }

            // Time update
            float timeElapsed = Time.time - startTime;
            finalTime = timeElapsed;
            int seconds = Mathf.FloorToInt(timeElapsed);
            int milliseconds = Mathf.FloorToInt((timeElapsed * 1000) % 1000);
            string textSec = seconds.ToString();
            string textMil = milliseconds.ToString();
            textForTime.text = "Time Spent (in second): " + textSec + "." + textMil;

            // Distance update
            int distInt = Mathf.FloorToInt(GlobalVariables.distance);
            int distFloat = Mathf.FloorToInt((GlobalVariables.distance * 1000) % 1000);
            string textDistInt = distInt.ToString();
            string textDistFloat = distFloat.ToString();
            textForDistance.text = "Distance:  " + textDistInt + "." + textDistFloat;
        }

        // Ending
        else
        {
            // Ending info
            textForCheck.enabled = true;
            int finalInt = Mathf.FloorToInt(finalTime);
            int finalMil = Mathf.FloorToInt(finalTime);
            textForCheck.text = "Finished! You take " + finalInt.ToString() + "." + finalMil.ToString() + " seconds!";
        }
    }
}
