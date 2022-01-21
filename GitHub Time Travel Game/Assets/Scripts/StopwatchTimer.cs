using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopwatchTimer : MonoBehaviour
{
    // Start is called before the first frame update
    float currentTime = 0f;
    float startingTime = 0f;
    public GameObject textDisplay;

    void Start()
    {
        currentTime = startingTime;
        textDisplay.transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1*Time.deltaTime;
        int totalSeconds = (int) currentTime;

        textDisplay.GetComponent<Text>().text = secondsToDisplay(totalSeconds);
    }

    string secondsToDisplay(int totalSeconds) {
        int minutes = totalSeconds/60;
        int seconds = totalSeconds % 60;
        
        string minutesString = minutes.ToString();
        string secondsString;

        if (seconds < 10)
            secondsString = $"0{seconds.ToString()}";
        else
            secondsString = seconds.ToString();

        
        return "0" + minutesString + ":" + secondsString;
    }

}
