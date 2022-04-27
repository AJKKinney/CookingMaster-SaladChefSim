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
        if (controller.locked == false)
        {
            if (playerControls.PlayerOneActions.Interact.WasPressedThisFrame() || playerControls.PlayerTwoActions.Interact.WasPressedThisFrame())
            {
                Interact();
            }
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

                //pickup plate
                if(inventory.carriedVegetables[0] == null && inventory.carriedMixture == null)
                {
                    if (inventory.GrabMixture(plate.currentMixture) == true)
                    {
                        plate.RemovePlate();
                    }
                }
                else if (inventory.carriedVegetables[0] != null)
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
                    inventory.DropMixture();
                }
            }
            //Trash Interaction
            else if (hit.transform.CompareTag("Trash"))
            {
                //Throw away carried vegetables
                if (inventory.carriedVegetables[0] != null)
                {
                    Debug.Log("Threw Away " + inventory.carriedVegetables[0].GetName());
                    inventory.DropVegetable();
                }
                //Throw away carried mixture
                else if(inventory.carriedMixture != null)
                {
                    Debug.Log("Threw Away " + inventory.carriedMixture.GetName());

                    //remove points if salad was thrown away
                    if(inventory.carriedMixture.salad == true)
                    {
                        ScoreTracker.instance.AddPoints(ScoreTracker.instance.penaltyForTossing, controller.id);
                    }
                    inventory.DropMixture();
                }
            }
            //Customer Interaction
            else if (hit.transform.CompareTag("Customer"))
            {
                CheckoutStation checkout = hit.collider.gameObject.GetComponent<CheckoutStation>();

                //Serve customer carried salad
                if (inventory.carriedMixture != null)
                {
                    if(inventory.carriedMixture.salad == true && checkout.customerWaiting == true)
                    {
                        //add points based on correctness and mix size
                        int mixSize = inventory.carriedMixture.vegetables.Count;

                        if(checkout.ServeCustomer(inventory.carriedMixture) == true)
                        {
                            ScoreTracker.instance.AddPoints(ScoreTracker.instance.basePointsAwarded + ScoreTracker.instance.additionalPerVeg * mixSize, controller.id);
                        }
                        else
                        {
                            ScoreTracker.instance.AddPoints(ScoreTracker.instance.penaltyForWrongSalad, controller.id);
                        }
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
                    if (ChopVegetable(choppingLocation))
                    {
                        inventory.DropVegetable();
                    }
                }
                //pickup mixture if hands are empty and not chopping
                else if(inventory.carriedMixture == null && choppingLocation.currentMixture != null && choppingLocation.choppingVegetable == null)
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
    public bool ChopVegetable(ChoppingLocation choppingLocation)
    {
        if (choppingLocation.owner == controller)
        {
            return choppingLocation.ChopVegetable(inventory.carriedVegetables[0]);
        }

        return false;
    }


    //Gizmos for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + Vector3.up + transform.rotation * Vector3.forward * offset, Vector3.down * rayLength);
    }
}
