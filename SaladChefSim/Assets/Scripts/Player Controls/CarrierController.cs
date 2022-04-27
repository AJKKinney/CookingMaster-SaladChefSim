using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enables Players to Pickup, Drop, Cut, and Throw Away Vegetables

[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(PlayerInventory))]
public class CarrierController : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerMovementController controller;
    private PlayerInventory inventory;

    [Header("Pick Up Settings")]
    public float rayLength = 0.5f;
    public float offset = 1f;

    //Initialize Component
    void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
        controller = GetComponent<PlayerMovementController>();
    }

    void Start()
    {
        playerControls = GetComponent<PlayerMovementController>().playerControls;
    }

    //Check for Interaction Keys
    void Update()
    {
        if(playerControls.PlayerOneActions.Interact.WasPressedThisFrame() || playerControls.PlayerTwoActions.Interact.WasPressedThisFrame())
        {
            Interact();
        }
    }

    public void Interact()
    {
        Ray ray = new Ray(transform.position + Vector3.up + transform.rotation * Vector3.forward * offset, Vector3.down * rayLength);

        //check for interaction
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength) == true)
        {
            //Vegetable Interaction
            if (hit.transform.CompareTag("Vegetable"))
            {
                //Pickup Vegetable
                TryPickupVeggie(hit);
            }
            else if (hit.transform.CompareTag("Plate"))
            {
                Plate plate = hit.collider.gameObject.GetComponent<Plate>();
                if (inventory.carriedVegetables[0] != null)
                {
                    //Store Vegetable on Plate
                    if (plate.StoreVegetable(inventory.carriedVegetables[0]) == true)
                    {
                        inventory.DropVegetable();
                    }
                    //if plate was full and inventory location is open pick up vegetable from plate
                    else if (inventory.carriedVegetables[1] == null && plate.currentVegetable != null)
                    {
                        inventory.AddVegetable(plate.currentVegetable);
                    }
                }
                //Add Mixture To Salad on Plate
                else if (inventory.carriedMixture != null)
                {
                    plate.AddMixture(inventory.carriedMixture, inventory);
                }
                //pickup Salad
                else
                {
                    inventory.GrabMixture(plate.currentMixture);
                    plate.RemovePlate();
                }
            }
            //Trash Interaction
            else if (hit.transform.CompareTag("Trash"))
            {
                //Throw away carried vegetables
                if (inventory.carriedVegetables[0] != null)
                {
                    inventory.DropVegetable();
                }
                //Throw away carried mixture
                else if(inventory.carriedMixture != null)
                {
                    inventory.DropMixture();
                }
            }
            //Customer Interaction
            else if (hit.transform.CompareTag("Customer"))
            {
                //Serve customer carried salad
                if(inventory.carriedMixture != null)
                {
                    if(inventory.carriedMixture.salad == true)
                    {
                        inventory.DropMixture();
                    }
                }
            }
            //Cutting Board Interaction
            else if (hit.transform.CompareTag("Chopping"))
            {
                ChoppingLocation choppingLocation = hit.collider.gameObject.GetComponent<ChoppingLocation>();

                //chop veggies on cutting board
                if (inventory.carriedVegetables[0] != null)
                {
                    ChopVegetable(choppingLocation);
                    inventory.DropVegetable();
                }
                //pickup mixture if hands are empty
                else if(inventory.carriedMixture == null && choppingLocation.currentMixture != null)
                {
                    inventory.GrabMixture(choppingLocation.currentMixture);
                    choppingLocation.RemoveMixture();
                }
            }
        }
    }

    //add hit veggie to inventory
    public void TryPickupVeggie(RaycastHit hitCollider)
    {
        Vegetable veggie = hitCollider.collider.gameObject.GetComponent<Vegetable>();
        inventory.AddVegetable(veggie);
    }

    //chop veggies at chop location
    public void ChopVegetable(ChoppingLocation choppingLocation)
    {
        if (choppingLocation.owner == controller)
        {
            choppingLocation.ChopVegetable(inventory.carriedVegetables[0]);
        }
    }


    //Gizmos for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + Vector3.up + transform.rotation * Vector3.forward * offset, Vector3.down * rayLength);
    }
}
