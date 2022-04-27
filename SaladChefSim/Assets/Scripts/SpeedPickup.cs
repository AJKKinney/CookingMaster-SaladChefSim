﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{

    private float timer;
    readonly private float duration = 30f;
    private PlayerMovementController targetPlayer;

    private void Update()
    {
        if(pickedUp)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                //reset speed
                targetPlayer.playerSpeed -= 50;
                //disable to prevent bugs
                pickedUp = false;
                //destroy pickup
                Destroy(this.gameObject);
            }
        }
    }

    internal override void PickUp(PlayerMovementController player)
    {
        targetPlayer = player;

        base.PickUp(targetPlayer);
        //increase speed
        player.playerSpeed += 50;
        //set timer
        timer = duration;
    }
}