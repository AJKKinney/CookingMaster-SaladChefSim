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

    void Update()
    {
        if(playerControls.PlayerOneActions.Interact.WasPressedThisFrame() || playerControls.PlayerTwoActions.Interact.WasPressedThisFrame())
        {
            Interact();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + Vector3.up + transform.rotation * Vector3.forward * offset, Vector3.down * rayLength);
    }

    public void Interact()
    {
        Ray ray = new Ray(transform.position + Vector3.up + transform.rotation * Vector3.forward * offset, Vector3.down * rayLength);

        //check for interaction
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength) == true)
        {
            if (hit.transform.CompareTag("Vegetable"))
            {
                Debug.Log("Grab Veg");
                TryPickupVeggie(hit);
            }
            else if (hit.transform.CompareTag("Plate"))
            {
                StoreVegetable();
                inventory.DropVegetable();
            }
            else if (hit.transform.CompareTag("Trash"))
            {
                if (inventory.carriedVegetables[0] != null)
                {
                    inventory.DropVegetable();
                }
            }
            else if (hit.transform.CompareTag("Customer"))
            {

            }
            else if (hit.transform.CompareTag("Chopping"))
            {
                if (inventory.carriedVegetables[0] != null)
                {
                    ChopVegetable();
                    StoreVegetable();
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

    public void ChopVegetable()
    {

    }

    public void StoreVegetable()
    {

    }

}
