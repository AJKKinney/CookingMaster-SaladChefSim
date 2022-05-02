using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Stores a Vegetable to be used later by a player
public class Plate : MonoBehaviour
{
    //the stored vegetable
    [HideInInspector]
    public Vegetable currentVegetable;

    //displays the stored vegetables
    private PlateGFXController gfxController;

    //GetGFXUpdater
    private void Awake()
    {
        gfxController = GetComponentInChildren<PlateGFXController>();
    }


    //stores a vegetable on the plate
    //returns true if the plate is available for storage
    public bool StoreVegetable(Vegetable veggie)
    {
        if(currentVegetable == null)
        {
            Debug.Log("Stored " + veggie.GetName() + " on the Plate.");
            currentVegetable = veggie;
            gfxController.UpdateGFX(veggie.GetID());
            return true;
        }

        return false;
    }


    //Removes a vegetable from the plate
    public void RemoveVegetable()
    {
        currentVegetable = null;
        gfxController.UpdateGFX();
    }
}
