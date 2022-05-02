using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enables Players to Pickup, Drop, Cut, and Throw Away Vegetables

[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerInventoryHUD))]
public class CarrierController : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerMovementController controller;
    private PlayerInventory inventory;
    private PlayerInventoryHUD inventoryHUD;

    [Header("Pick Up Settings")]
    public float rayLength = 0.5f;
    public float offset = 1f;

    //Initialize Component
    void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
        controller = GetComponent<PlayerMovementController>();
        inventoryHUD = GetComponent<PlayerInventoryHUD>();
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
                Vegetable veg = hit.collider.gameObject.GetComponent<Vegetable>();

                //Pickup Vegetable
                if (TryPickupVeggie(hit)) {
                    inventoryHUD.CreateCarriedIcon(veg.GetID());
                }
            }


            else if (hit.transform.CompareTag("Plate"))
            {
                Plate plate = hit.collider.gameObject.GetComponent<Plate>();

                //if plate was full and inventory location is open pick up vegetable from plate
                if (inventory.carriedVegetables[1] == null && plate.currentVegetable != null && inventory.carriedMixture == null)
                {
                    inventory.AddVegetable(plate.currentVegetable);
                    inventoryHUD.CreateCarriedIcon(plate.currentVegetable.GetID());
                    plate.RemoveVegetable();
                }
                else if (inventory.carriedVegetables[0] != null && plate.currentVegetable == null)
                {
                    //Store Vegetable on Plate
                    if (plate.StoreVegetable(inventory.carriedVegetables[0]) == true)
                    {
                        inventory.DropVegetable();
                        inventoryHUD.RemoveIcon();
                    }
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
                    inventoryHUD.RemoveIcon();
                }
                //Throw away carried mixture
                else if(inventory.carriedMixture != null)
                {
                    Debug.Log("Threw Away " + inventory.carriedMixture.GetName());


                    ScoreTracker.instance.AddPoints(ScoreTracker.instance.penaltyForTossing, controller.id);

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
                    if(checkout.customerWaiting == true)
                    {
                        checkout.ServeCustomer(inventory.carriedMixture, controller.id);
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
                        inventoryHUD.RemoveIcon();
                    }
                }
                //put mixture on cutting board
                else if(inventory.carriedMixture != null)
                {
                    Debug.Log("Placed " + inventory.carriedMixture.GetName() + "On Cutting Board");
                    choppingLocation.PlaceMixture(inventory.carriedMixture);
                    inventory.DropMixture();
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
    public bool TryPickupVeggie(RaycastHit hitCollider)
    {
        Vegetable veggie = hitCollider.collider.gameObject.GetComponent<Vegetable>();
        return inventory.AddVegetable(veggie);
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
