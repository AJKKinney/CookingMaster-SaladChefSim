using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the game timer and ends the game when necessary by freezing players
public class GameTimer : MonoBehaviour
{

    static public GameTimer instance;
    public float gameLength = 120f;
    readonly private float countdown = 3f;
    [HideInInspector]
    public float playerOneTimeRemaining;
    [HideInInspector]
    public float playerTwoTimeRemaining;

    private bool started = false;

    [Header("Players")]
    public PlayerMovementController playerOne;
    public PlayerMovementController playerTwo;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        //lock players for countdown
        LockPlayer(1);
        LockPlayer(2);


        //set timer
        playerOneTimeRemaining = gameLength + countdown;
        playerTwoTimeRemaining = playerOneTimeRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        Tick();

        if (playerOneTimeRemaining > 0 && playerOneTimeRemaining < gameLength)
        {
            UnlockPlayers();

        }
    }

    //runs timer for players
    private void Tick()
    {
        if (playerOneTimeRemaining > 0)
        {
            playerOneTimeRemaining -= Time.deltaTime;
        }
        else
        {
            LockPlayer(1);
        }

        if (playerTwoTimeRemaining > 0)
        {
            playerTwoTimeRemaining -= Time.deltaTime;
        }
        else
        {
            LockPlayer(2);
        }
    }

    private void LockPlayer(int player)
    {
        if (player == 1)
        {
            playerOne.locked = true;
        }
        else if (player == 2)
        {
            playerTwo.locked = true;
        }
    }

    public void AddTime(float time, int player)
    {
        if (player == 1)
        {
            playerOneTimeRemaining += time;
        }
        else if (player == 2)
        {
            playerTwoTimeRemaining += time;
        }
    }

    private void UnlockPlayers()
    {
        playerOne.locked = false;
        playerTwo.locked = false;
    }
}
