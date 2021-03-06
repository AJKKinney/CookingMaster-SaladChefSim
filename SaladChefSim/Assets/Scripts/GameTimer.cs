using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the game timer and ends the game when necessary by freezing players
public class GameTimer : MonoBehaviour
{

    static public GameTimer instance;

    [Header("Timer")]
    public float gameLength = 120f;

    [Header("Players")]
    public PlayerMovementController playerOne;
    public PlayerMovementController playerTwo;

    [HideInInspector]
    public bool started = false;
    [HideInInspector]
    public float playerOneTimeRemaining;
    [HideInInspector]
    public float playerTwoTimeRemaining;

    private bool paused = false;

    readonly private float countdown = 3f;

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
        if (paused == false)
        {
            Tick();

            //started bool added to prevent from constantly firing
            if (playerOneTimeRemaining > 0 && playerOneTimeRemaining < gameLength && started == false)
            {
                UnlockPlayers();
                started = true;
            }
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

    //stops the timers
    public void Pause()
    {
        paused = true;
        LockPlayer(1);
        LockPlayer(2);
    }

    //resumes the timers
    public void Unpause()
    {
        paused = false;
        UnlockPlayers();
    }
}
