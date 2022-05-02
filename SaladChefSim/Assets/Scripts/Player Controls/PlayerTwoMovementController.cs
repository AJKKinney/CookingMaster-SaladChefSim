using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Movement Controller for Player Two
public class PlayerTwoMovementController : PlayerMovementController
{
    //Enable Player 2 Input
    public override void EnableControls()
    {
        base.EnableControls();
        playerControls.PlayerTwoActions.Enable();
        this.id = 2;
    }

    void Update()
    {
        //update movement input
        moveVector = new Vector3(playerControls.PlayerTwoActions.Movement.ReadValue<Vector2>().x, 0, playerControls.PlayerTwoActions.Movement.ReadValue<Vector2>().y);

        //check for running
        if (playerControls.PlayerTwoActions.Cancel.ReadValue<float>() > 0)
        {
            running = true;
        }
        else
        {
            running = false;
        }
    }
}
