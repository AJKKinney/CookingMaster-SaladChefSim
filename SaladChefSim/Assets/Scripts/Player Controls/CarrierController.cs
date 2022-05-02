using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When you have time break this class up into single responsibilities - 5/2/2022 AK

//Handles player interaction with game elements
[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerInventoryHUD))]
[RequireComponent(typeof(PlayerChoppingHUD))]
public class CarrierController : MonoBehaviour
{
    [Header("Pick Up Settings")]
    public float rayLength = 0.5f;
    public float offset = 1f;

    //Get player input
    private PlayerControls playerControls;
    //Get player controller info
    private PlayerMovementController controller;
    //stores carried item information
    private PlayerInventory inventory;
    //shows carried item info to the player
    private PlayerInventoryHUD inventoryHUD;
    //shows chopped veggie timer
    private PlayerChoppingHUD choppingHUD;


    void Awake()
    {
        //Initialize Component
        inventory = GetComponent<PlayerInventory>();
        controller = GetComponent<PlayerMovementController>();
        inventoryHUD = GetComponent<PlayerInventoryHUD>();
        choppingHUD = GetComponent<PlayerChoppingHUD>();
    }

    void Start()
    {
        playerControls = GetComponent<PlayerMovementController>().playerControls;
    }

    void Update()
    {
        //Check for Interaction Keys
        if (controller.locked == false)
        {
            if (playerControls.PlayerOneActions.Interact.WasPressedThisFrame() || playerControls.PlayerTwoActions.Interact.WasPressedThisFrame())
            {
                Interact();
            }
        }
    }

    //called to check for interactable elements with a raycast and handles the case appropriately
    public void Interact()
    {
        //create interaction ray
        Ray ray = new Ray(transform.position + Vector3.up + transform.rotation * Vector3.forward * offset, Vector3.down * rayLength);

        //check for interactable elements
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength) == true)
        {
            //Vegetable Interaction
            if (hit.transform.CompareTag("Vegetable"))
            {
                //get vegetable
                Vegetable veg = hit.collider.gameObject.GetComponent<Vegetable>();

                //Pickup Vegetable
                if (inventory.AddVegetable(veg)) {
                    inventoryHUD.CreateCarriedIcon(veg.GetID());
                }
            }

            //Plate Interaction
            else if (hit.transform.CompareTag("Plate"))
            {
                //get plate
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
                        inventory.RemoveVegetable();
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
                    
                    //remove vegetable from inventory
                    inventory.RemoveVegetable();
                    //update ui
                    inventoryHUD.RemoveIcon();
                }
                //Throw away carried mixture
                else if(inventory.carriedMixture != null)
                {
                    Debug.Log("Threw Away " + inventory.carriedMixture.GetName());

                    //lose points
                    ScoreTracker.instance.AddPoints(ScoreTracker.instance.penaltyForTossing, controller.id);

                    //remove mixture from inventory
                    inventory.RemoveMixture();
                }
            }

            //Customer Interaction
            else if (hit.transform.CompareTag("Customer"))
            {
                CheckoutStation checkout = hit.collider.gameObject.GetComponent<CheckoutStation>();

                //Serve customer carried salad if they are waiting
                if (inventory.carriedMixture != null && checkout.customerWaiting == true)
                {
                    //serve mixture
                    checkout.ServeCustomer(inventory.carriedMixture, controller.id);
                    //remove mixture from inventory
                    inventory.RemoveMixture();

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
                        choppingHUD.ChopVeggies(inventory.carriedVegetables[0].chopTime);
                        inventory.RemoveVegetable();
                        inventoryHUD.RemoveIcon();
                    }
                }
                //put mixture on cutting board
                else if(inventory.carriedMixture != null)
                {
                    Debug.Log("Placed " + inventory.carriedMixture.GetName() + "On Cutting Board");
                    choppingLocation.AddMixture(inventory.carriedMixture);
                    inventory.RemoveMixture();
                }
                //pickup mixture if hands are empty and not chopping
                else if(inventory.carriedMixture == null && choppingLocation.currentMixture != null && choppingLocation.choppingVegetable == null)
                {
                    inventory.AddMixture(choppingLocation.currentMixture);
                    choppingLocation.RemoveMixture();
                }
            }
        }
    }

    //chop veggies at chop location
    //returns true if the player is the owner
    public bool ChopVegetable(ChoppingLocation choppingLocation)
    {
        if (choppingLocation.owner == controller)
        {
            return choppingLocation.ChopVegetable(inventory.carriedVegetables[0]);
        }

        return false;
    }


    //Display Gizmos for Set-up and Debugging when selected 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + Vector3.up + transform.rotation * Vector3.forward * offset, Vector3.down * rayLength);
    }
}
