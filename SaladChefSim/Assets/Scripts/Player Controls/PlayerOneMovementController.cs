using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Movement Controller for Player One
public class PlayerOneMovementController : PlayerMovementController
{
    //Enable Player 1 Input
    public override void EnableControls()
    {
        base.EnableControls();
        playerControls.PlayerOneActions.Enable();
        this.id = 1;
    }

    void Update()
    {
        //update movement input
        moveVector = new Vector3(playerControls.PlayerOneActions.Movement.ReadValue<Vector2>().x, 0, playerControls.PlayerOneActions.Movement.ReadValue<Vector2>().y);

        //check for running
        if (playerControls.PlayerOneActions.Cancel.ReadValue<float>() > 0)
        {
            running = true;
        }
        else
        {
            running = false;
        }
    }
}
