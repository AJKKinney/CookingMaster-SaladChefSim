using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneMovementController : PlayerMovementController
{
    //Enable Player 1 Input
    public override void EnableControls()
    {
        base.EnableControls();
        playerControls.PlayerOneActions.Enable();
        this.id = 1;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = new Vector3(playerControls.PlayerOneActions.Movement.ReadValue<Vector2>().x, 0, playerControls.PlayerOneActions.Movement.ReadValue<Vector2>().y);

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
