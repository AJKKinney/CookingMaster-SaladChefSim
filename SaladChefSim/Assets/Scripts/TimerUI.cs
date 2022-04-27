using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Sets timer text to match time remaining
public class TimerUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI playerOneTimer;
    public TextMeshProUGUI playerTwoTimer;
    public TextMeshProUGUI countdownNumbers;
    public GameObject playerOneWinScreen;
    public GameObject playerTwoWinScreen;
    public GameObject tiedWinScreen;
    public GameObject gameHUD;

    //Create Coundown Numbers
    void Awake()
    {
        GameObject newNumbers = Instantiate(countdownNumbers.gameObject, countdownNumbers.transform.parent);
        countdownNumbers = newNumbers.GetComponent<TextMeshProUGUI>();
        countdownNumbers.gameObject.SetActive(true);
    }

    //set start time
    public void Start()
    {
        playerOneTimer.text = Mathf.CeilToInt(GameTimer.instance.gameLength).ToString();
        playerTwoTimer.text = Mathf.CeilToInt(GameTimer.instance.gameLength).ToString();
    }

    // Update is called once per frame
    void Update()
    {

        UITick();

        //End Game Screen
        if(GameTimer.instance.playerOneTimeRemaining <= 0 && GameTimer.instance.playerTwoTimeRemaining <= 0)
        {
            if (ScoreTracker.instance.getScore(1) > ScoreTracker.instance.getScore(2))
            {
                Debug.Log("Player One Wins");
                playerOneWinScreen.SetActive(true);
            }
            else if (ScoreTracker.instance.getScore(1) < ScoreTracker.instance.getScore(2))
            {
                Debug.Log("Player Two Wins");
                playerTwoWinScreen.SetActive(true);
            }
            else
            {
                Debug.Log("The Players Tied");
                tiedWinScreen.SetActive(true);
            }

            gameHUD.SetActive(false);
        }
    }

    void UITick()
    {
        //player One

        //round up for prettier numbers
        int timeRemaining = Mathf.CeilToInt(GameTimer.instance.playerOneTimeRemaining);

        //update timer if game is started
        if (timeRemaining < GameTimer.instance.gameLength)
        {
            playerOneTimer.text = timeRemaining.ToString();
        }
        else if(countdownNumbers != null)
        {
            int countdown = timeRemaining - (int)GameTimer.instance.gameLength;

            if (countdown == 0)
            {
                countdownNumbers.text = "GO!";
                Destroy(countdownNumbers.gameObject, 1);
            }
            else
            {
                countdownNumbers.text = (timeRemaining - (int)GameTimer.instance.gameLength).ToString();
            }
        }


        //player Two

        //round up for prettier numbers
        timeRemaining = Mathf.CeilToInt(GameTimer.instance.playerTwoTimeRemaining);

        //update timer if game is started
        if (timeRemaining < GameTimer.instance.gameLength)
        {
            playerTwoTimer.text = timeRemaining.ToString();
        }
    }
}