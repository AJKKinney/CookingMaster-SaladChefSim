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
    public float timeRemaining;

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

        timeRemaining = gameLength + countdown;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (timeRemaining > 0 && timeRemaining < gameLength)
        {
            UnlockPlayers();

        }
        else
        {
            LockPlayers();
        }
    }

    private void LockPlayers()
    {
        playerOne.locked = true;
        playerTwo.locked = true;
    }

    private void UnlockPlayers()
    {
        playerOne.locked = false;
        playerTwo.locked = false;
    }
}
