using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//When you Have time break this class into single responsibility classes - 5/2/2022

[RequireComponent(typeof(GameSFXController))]

//Tracks the players scores and updates the ui

public class ScoreTracker : MonoBehaviour
{

    public static ScoreTracker instance;

    [Header("Scoreboard")]
    public TextMeshProUGUI playerOneScoreboard;
    public TextMeshProUGUI playerTwoScoreboard;

    [Header("Scoring")]
    readonly public int basePointsAwarded = 30;
    readonly public int additionalPerVeg = 10;
    readonly public int penaltyForTossing = -15;
    readonly public int penaltyForLeaver = -25;
    readonly public float penaltyForWrongSalad = 0.2f;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private GameSFXController sfx;

    //initialization
    void Awake()
    {
        instance = this;
        sfx = GetComponent<GameSFXController>();
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

        //play sfx
        if (points >= 0)
        {
            sfx.PlayScoreSFX();
        }
        else
        {
            sfx.PlayPenaltySFX();
        }

        UpdateUI();
    }

    //overload adds points for both players to the scoreboard
    public void AddPoints(int points)
    {
            playerOneScore += points;
            playerTwoScore += points;

        //play sfx
        if (points >= 0)
        {
            sfx.PlayScoreSFX();
        }
        else
        {
            sfx.PlayPenaltySFX();
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
