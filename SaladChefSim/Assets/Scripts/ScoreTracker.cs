using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{

    public static ScoreTracker instance;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;

    [Header("Scoreboard")]

    public TextMeshProUGUI playerOneScoreboard;
    public TextMeshProUGUI playerTwoScoreboard;

    [Header("Scoring")]
    readonly public int basePointsAwarded = 30;
    readonly public int additionalPerVeg = 10;
    readonly public int penaltyForTossing = -15;
    readonly public int penaltyForLeaver = -25;
    readonly public int additionalForAngry = -10;
    readonly public float penaltyForWrongSalad = 0.2f;


    //initialization
    void Awake()
    {
        instance = this;
    }

    //Gets The Score of a Player
    public int getScore(int player)
    {
        if (player == 1)
        {
            return playerOneScore;
        }
        else if(player == 2)
        {
            return playerTwoScore;
        }

        return 0;
    }

    //adds points for the player to the scoreboard
    public void AddPoints(int points, int player)
    {
        if (player == 1)
        {
            playerOneScore += points;
        }
        else if (player == 2)
        {
            playerTwoScore += points;
        }

        UpdateUI();
    }

    //updates the scoreboard UI
    public void UpdateUI()
    {
        playerOneScoreboard.text = playerOneScore.ToString();
        playerTwoScoreboard.text = playerTwoScore.ToString();
    }

}
