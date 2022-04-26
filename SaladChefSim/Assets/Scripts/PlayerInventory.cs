using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player Inventory controls picking up and dropping of vegetables, combinations, and salads.
public class PlayerInventory : MonoBehaviour
{
    [HideInInspector]
    public Vegetable[] carriedVegetables = new Vegetable[2];
    [HideInInspector]
    public Mixture carriedMixture;

    //add a veggie to your inventory
    public bool AddVegetable(Vegetable veggie)
    {

        for(int i = 0; i < carriedVegetables.Length; i++)
        {
            if(carriedVegetables[i] == null)
            {
                Debug.Log("Picked up " + veggie.GetName());
                carriedVegetables[i] = veggie.Grab();
                return true;
            }
        }

        Debug.Log("Inventory is Full");
        return false;
    }

    //remove a veggie from your inventory
    //FIFO
    public bool DropVegetable()
    {
        bool dropped = false;

        for (int i = 0; i < carriedVegetables.Length; i++)
        {
            if(i == 0)
            {
                if (carriedVegetables[i] != null)
                {
                    dropped = true;
                }
            }
            else
            {
                carriedVegetables[i - 1] = carriedVegetables[i];

                if(i == carriedVegetables.Length - 1)
                {
                    carriedVegetables[i] = null;
                }
            }
        }

        return dropped;
    }

    //picks up a mixture if not already carrying veggies
    public bool GrabMixture(Mixture mixture)
    {
        if (carriedVegetables[0] == null)
        {
            carriedMixture = mixture;
            return true;
        }

        return false;
    }

    //removes the carried mixture
    public void DropMixture()
    {
        carriedMixture = null;
    }
}