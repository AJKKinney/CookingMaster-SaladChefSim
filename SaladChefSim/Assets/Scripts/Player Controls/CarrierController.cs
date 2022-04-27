using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enables Players to Pickup, Drop, Cut, and Throw Away Vegetables

[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(PlayerInventory))]
public class CarrierController : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerInventory inventory;

    [Header("Pick Up Settings")]
    public float rayLength = 0.5f;
    public float offset = 1f;

    //Initialize Component
    void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
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
                //Store Vegetable on Plate
                if (inventory.carriedVegetables[0] != null)
                {
                    Plate plate = hit.collider.gameObject.GetComponent<Plate>();
                    if (plate.StoreVegetable(inventory.carriedVegetables[0]) == true)
                    {
                        inventory.DropVegetable();
                    }
                }
                //Add Mixture To Salad on Plate
                else if(inventory.carriedMixture != null)
                {
                    Plate plate = hit.collider.gameObject.GetComponent<Plate>();
                    plate.AddMixture(inventory.carriedMixture, inventory);
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
                //chop veggies on cutting board
                if (inventory.carriedVegetables[0] != null)
                {
                    ChopVegetable(hit);
                    inventory.DropVegetable();
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
    public void ChopVegetable(RaycastHit hitCollider)
    {
        ChoppingLocation choppingLocation = hitCollider.collider.gameObject.GetComponent<ChoppingLocation>();
        choppingLocation.ChopVegetable(inventory.carriedVegetables[0]);
    }


    //Gizmos for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + Vector3.up + transform.rotation * Vector3.forward * offset, Vector3.down * rayLength);
    }
}
