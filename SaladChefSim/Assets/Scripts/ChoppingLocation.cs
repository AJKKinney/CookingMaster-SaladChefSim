using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SaladGFXController))]

//Controls the chopping of vegetables and creation of mixtures
public class ChoppingLocation : MonoBehaviour
{
    [Header("Team")]
    public PlayerMovementController owner;

    //the currently stored mixture at the chopping location
    [HideInInspector]
    public Mixture currentMixture;
    //the vegetable currently being chopped
    [HideInInspector]
    public Vegetable choppingVegetable;
    //the timer for chopping vegetables
    private float chopTime;
    private SaladGFXController saladGFX;


    private void Awake()
    {
        //initialize
        saladGFX = GetComponent<SaladGFXController>();
    }


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

            //display gfx
            saladGFX.displaySalad(currentMixture);
            owner.locked = false;
            choppingVegetable = null;
        }
    }


    //Starts Chopping the Vegetable passed into the function
    public bool ChopVegetable(Vegetable veggie)
    {
        if (chopTime <= 0)
        {
            Debug.Log("Start chopping");
            owner.locked = true;
            choppingVegetable = veggie;
            chopTime = veggie.chopTime;
            return true;
        }
        return false;
    }


    //removes the mixture stored at the chopping location
    public void RemoveMixture()
    {
        saladGFX.HideSalad();
        currentMixture = null;
    }


    //adds a mixture to the chopping location
    public void AddMixture(Mixture mixture)
    {
        saladGFX.displaySalad(mixture);
        currentMixture = mixture;
    }
}
