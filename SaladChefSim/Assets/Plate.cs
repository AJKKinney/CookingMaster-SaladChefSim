using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [HideInInspector]
    public Mixture currentMixture;
    [HideInInspector]
    public Vegetable currentVegetable;

    //stores a vegetable on the plate
    //returns true if the plate is available for storage
    public bool StoreVegetable(Vegetable veggie)
    {
        if(currentMixture == null && currentVegetable == null)
        {
            Debug.Log("Stored " + veggie.GetName() + " on the Plate.");
            currentVegetable = veggie;
            return true;
        }

        return false;
    }

    //Adds a mixture to the plate
    //Pass in inventory for retrieval of stored vegetable
    public void AddMixture(Mixture mixture, PlayerInventory inventory)
    {
        if(currentMixture == null)
        {
            if(currentVegetable != null)
            {
                Debug.Log("Picked up " + currentVegetable.GetName() + " from the Plate.");
                inventory.AddVegetable(currentVegetable);
                currentVegetable = null;
            }

            Debug.Log("Added " + mixture.GetName() + " to the Empty Plate.");
            mixture.salad = true;
            currentMixture = mixture;
        }
        else
        {
            for(int i = 0; i < mixture.vegetables.Count; i++)
            {
                Debug.Log("Added " + mixture.GetName() + " to the " + currentMixture.GetName() + " on the Plate.");
                currentMixture.AddVeggie(mixture.vegetables[i]);
            }
        }
    }

    public void RemovePlate()
    {
        currentMixture = null;
    }
}
