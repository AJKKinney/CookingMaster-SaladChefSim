using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Sets timer text to match time remaining
public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    // Update is called once per frame
    void Update()
    {
        //round up for prettier numbers
        int timeRemaining = Mathf.CeilToInt(GameTimer.instance.timeRemaining);

        //update timer if game is started
        if (timeRemaining < GameTimer.instance.gameLength)
        {
            textMesh.text = timeRemaining.ToString();
        }
    }
}
