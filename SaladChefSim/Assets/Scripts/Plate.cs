using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{

    [HideInInspector]
    public Vegetable currentVegetable;

    //stores a vegetable on the plate
    //returns true if the plate is available for storage
    public bool StoreVegetable(Vegetable veggie)
    {
        if(currentVegetable == null)
        {
            Debug.Log("Stored " + veggie.GetName() + " on the Plate.");
            currentVegetable = veggie;
            return true;
        }

        return false;
    }
}
