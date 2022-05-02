using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Gives a time boost when a player walks over the gameobject
public class TimePickup : Pickup
{

    private readonly int time = 10;


    internal override void PickUp(PlayerMovementController player)
    {
        base.PickUp(player);
        //increase score
        GameTimer.instance.AddTime(time, player.id);
        Destroy(this.gameObject);
    }
}
