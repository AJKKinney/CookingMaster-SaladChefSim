using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    Collider collider;
    public GameObject powerupGFX;
    PlayerMovementController pickerUpper;
    internal bool pickedUp = false;

    //initialize
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    //when player enters collider trigger
    private void OnTriggerEnter(Collider other)
    {

        if(other.TryGetComponent<PlayerMovementController>(out pickerUpper))
        {
            PickUp(pickerUpper);
        }
    }

    //pickup object
    internal virtual void PickUp(PlayerMovementController player)
    {
        Debug.Log("Player " + player + " picked up " + name);

        //disable collider and gfx for ability to work

        powerupGFX.SetActive(false);
        collider.enabled = false;
        pickedUp = true;
    }
}
