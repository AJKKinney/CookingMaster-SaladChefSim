using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//This class controls basic directional movement for players

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{

    private Rigidbody playerRigidbody;
    internal PlayerControls playerControls;
    internal Vector3 moveVector;

    [Header("Player Movement")]
    public float playerSpeed = 100f;

    //Initialize Component
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        EnableControls();
    }

    //Fixed Update is called once every fixed timestep
    private void FixedUpdate()
    {
        
        if(moveVector != Vector3.zero)
        {
            //move player
            playerRigidbody.velocity = moveVector * playerSpeed * Time.fixedDeltaTime;

            //face direction
            transform.LookAt(transform.position + moveVector);
        }
    }

    public virtual void EnableControls()
    {
        //Enable Controls for Player
        playerControls = new PlayerControls();
    }
}
