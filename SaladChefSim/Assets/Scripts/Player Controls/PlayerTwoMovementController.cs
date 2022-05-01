using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoMovementController : PlayerMovementController
{
    //Enable Player 2 Input
    public override void EnableControls()
    {
        base.EnableControls();
        playerControls.PlayerTwoActions.Enable();
        this.id = 2;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = new Vector3(playerControls.PlayerTwoActions.Movement.ReadValue<Vector2>().x, 0, playerControls.PlayerTwoActions.Movement.ReadValue<Vector2>().y);

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
