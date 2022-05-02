using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimationManager))]
[RequireComponent(typeof(InteractionSFXController))]

//Player Inventory controls picking up and dropping of vegetables, combinations, and salads.
public class PlayerInventory : MonoBehaviour
{

    public Vegetable[] carriedVegetables = new Vegetable[2];
    public Mixture carriedMixture;

    private PlayerAnimationManager animManager;
    private InteractionSFXController sfx;
    private SaladGFXController saladGFX;


    private void Awake()
    {
        animManager = GetComponent<PlayerAnimationManager>();
        sfx = GetComponent<InteractionSFXController>();
        saladGFX = GetComponent<SaladGFXController>();
    }


    //add a veggie to your inventory
    public bool AddVegetable(Vegetable veggie)
    {
        if (carriedMixture == null)
        {
            for (int i = 0; i < carriedVegetables.Length; i++)
            {
                if (carriedVegetables[i] == null)
                {
                    Debug.Log("Picked up " + veggie.GetName());
                    carriedVegetables[i] = veggie;

                    //play sfx
                    sfx.PlayPickUpSFX();

                    return true;
                }
            }

            Debug.Log("Inventory is Full");

        }
        else
        {
            Debug.Log("Already Carrying a Mixture");
        }

        return false;
    }


    //remove a veggie from your inventory
    //FIFO
    public bool RemoveVegetable()
    {
        bool dropped = false;

        for (int i = 0; i < carriedVegetables.Length; i++)
        {
            if(i == 0)
            {
                if (carriedVegetables[i] != null)
                {
                    dropped = true;
                    //play sfx
                    sfx.PlayPutDownSFX();
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
    public bool AddMixture(Mixture mixture)
    {
        if (mixture != null)
        {
            if (carriedVegetables[0] == null)
            {
                Debug.Log("Picked Up " + mixture.GetName());
                carriedMixture = mixture;
                // add salad to hands
                animManager.AddSalad(mixture);


                //play sfx
                sfx.PlayPickUpSFX();

                return true;
            }
        }

        return false;
    }


    //removes the carried mixture
    public void RemoveMixture()
    {
        carriedMixture = null;

        //play sfx
        sfx.PlayPutDownSFX();

        animManager.RemoveSalad();
    }
}
