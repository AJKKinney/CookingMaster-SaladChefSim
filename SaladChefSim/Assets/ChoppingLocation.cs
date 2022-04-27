using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingLocation : MonoBehaviour
{
    [HideInInspector]
    public Mixture currentMixture;
    private Vegetable choppingVegetable;
    private float chopTime;

    public PlayerMovementController owner;

    // Update is called once per frame
    void Update()
    {
        //Chop Timer
        if (chopTime > 0f)
        {
            Debug.Log("Chopping " + choppingVegetable.GetName() + ": " + chopTime + " Seconds Remaining.");

            chopTime -= Time.deltaTime;
        }
        else if(choppingVegetable != null)
        {
            if (currentMixture == null)
            {
                Debug.Log("Created New " + choppingVegetable.GetName() + " Mixture.");
                currentMixture = new Mixture(choppingVegetable);
            }
            else
            {
                Debug.Log("Added " + choppingVegetable.GetName() + " to the " + currentMixture.GetName() + " on the Cutting Board.");
                currentMixture.AddVeggie(choppingVegetable);
            }
            choppingVegetable = null;
            owner.locked = false;
        }
    }

    //Start Chopping Vegetable
    public void ChopVegetable(Vegetable veggie)
    {
        owner.locked = true;
        choppingVegetable = veggie;
        chopTime = veggie.chopTime;
    }
}
