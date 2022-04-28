using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Sets timer text to match time remaining

//
[RequireComponent(typeof(WinScreenController))]
public class TimerUI : MonoBehaviour
{
    [Header("HUD Elements")]
    public TextMeshProUGUI playerOneTimer;
    public TextMeshProUGUI playerTwoTimer;
    public TextMeshProUGUI countdownNumbers;
    public GameObject gameHUD;

    private WinScreenController winScreen;

    private bool winner = false;


    //Create Coundown Numbers
    void Awake()
    {
        GameObject newNumbers = Instantiate(countdownNumbers.gameObject, countdownNumbers.transform.parent);
        countdownNumbers = newNumbers.GetComponent<TextMeshProUGUI>();
        countdownNumbers.gameObject.SetActive(true);

        //get win screen controller
        winScreen = GetComponent<WinScreenController>();
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
        //only run so long as a winner has not been declared
        if (winner == false)
        {
            UITick();

            //End Game Screen
            if (GameTimer.instance.playerOneTimeRemaining <= 0 && GameTimer.instance.playerTwoTimeRemaining <= 0)
            {

                gameHUD.SetActive(false);
                winScreen.DeclareWinner();
                winner = true;
            }
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
