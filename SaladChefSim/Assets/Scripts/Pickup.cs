using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    Collider pickupCollider;
    public GameObject powerupGFX;
    PlayerMovementController pickerUpper;
    internal bool pickedUp = false;
    public int owner;

    //initialize
    private void Awake()
    {
        pickupCollider = GetComponent<Collider>();
    }

    //when player enters collider trigger
    private void OnTriggerEnter(Collider other)
    {

        if(other.TryGetComponent<PlayerMovementController>(out pickerUpper))
        {
            if (pickerUpper.id == owner)
            {
                PickUp(pickerUpper);
            }
        }
    }

    //pickup object
    internal virtual void PickUp(PlayerMovementController player)
    {
        Debug.Log("Player " + player + " picked up " + name);

        //disable collider and gfx for ability to work

        powerupGFX.SetActive(false);
        pickupCollider.enabled = false;
        pickedUp = true;
    }
}
