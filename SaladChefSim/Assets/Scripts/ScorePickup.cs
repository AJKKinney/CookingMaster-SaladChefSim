using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : Pickup
{

    private readonly int score = 10;

    internal override void PickUp(PlayerMovementController player)
    {


        base.PickUp(player);
        //increase score
        ScoreTracker.instance.AddPoints(score, player.id);
        Destroy(this.gameObject);
    }
}
