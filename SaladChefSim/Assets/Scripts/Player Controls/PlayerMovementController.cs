using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This class controls movement for players

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerAnimationManager))]
public class PlayerMovementController : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 100f;
    public float playerRunSpeed = 200f;

    //the players id e.g. Player 1/ Player 2
    [HideInInspector]
    public int id = 0;

    //locks the players movement when true
    [HideInInspector]
    public bool locked = false;

    //gets player input
    internal PlayerControls playerControls;
    //animates the player character
    internal PlayerAnimationManager animationManager;
    //the players move vectore
    internal Vector3 moveVector;
    //increases player move speed when true
    internal bool running;

    //the players rigidbody
    private Rigidbody playerRigidbody;


    private void Awake()
    {
        //Initialize
        playerRigidbody = GetComponent<Rigidbody>();
        //Enables the players controls
        EnableControls();
        animationManager = GetComponent<PlayerAnimationManager>();
    }


    private void FixedUpdate()
    {
        //stop movement when locked
        if (locked == true)
        {
            animationManager.SetWalkSpeed(0);
            return;
        }

        //when player is moving
        if (moveVector != Vector3.zero)
        {
            if (running == true)
            {
                //move player with runspeed
                playerRigidbody.velocity = moveVector * playerRunSpeed * Time.fixedDeltaTime;
            }
            else
            {
                //move player
                playerRigidbody.velocity = moveVector * playerSpeed * Time.fixedDeltaTime;
            }

            //face direction
            transform.LookAt(transform.position + moveVector);
        }

        if (animationManager != null)
        {
            //animate player
            animationManager.SetWalkSpeed(playerRigidbody.velocity.magnitude / 2f);
        }
    }

    //Enable Controls for Player
    public virtual void EnableControls()
    {
        playerControls = new PlayerControls();
    }
}
