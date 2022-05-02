using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plate : MonoBehaviour
{

    [HideInInspector]
    public Vegetable currentVegetable;

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

    public void RemoveVegetable()
    {
        currentVegetable = null;
        gfxController.UpdateGFX();
    }
}
