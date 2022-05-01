using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This class controls basic directional movement for players

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerAnimationManager))]
public class PlayerMovementController : MonoBehaviour
{

    private Rigidbody playerRigidbody;
    internal PlayerControls playerControls;
    internal PlayerAnimationManager animationManager;
    internal Vector3 moveVector;
    internal bool running;
    [HideInInspector]
    public int id = 0;

    [HideInInspector]
    public bool locked = false;

    [Header("Player Movement")]
    public float playerSpeed = 100f;
    public float playerRunSpeed = 200f;

    //Initialize Component
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        EnableControls();
        animationManager = GetComponent<PlayerAnimationManager>();
    }


    //Fixed Update is called once every fixed timestep
    private void FixedUpdate()
    {
        if (locked == false)
        {
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
        else
        {
            animationManager.SetWalkSpeed(0);
        }


    }

    public virtual void EnableControls()
    {
        //Enable Controls for Player
        playerControls = new PlayerControls();
    }
}
