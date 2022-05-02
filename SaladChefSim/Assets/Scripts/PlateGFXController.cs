using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//makes the vegetable stored on the plate visible
public class PlateGFXController : MonoBehaviour
{
    public GameObject[] veggieGFX;

    //stores the reference to the GFX gameobject
    [HideInInspector]
    public GameObject currentVeggie;


    //updates gfx
    public void UpdateGFX(int VeggieID)
    {
        if(currentVeggie != null)
        {
            Destroy(currentVeggie);
        }

        currentVeggie = Instantiate(veggieGFX[VeggieID], transform);
    }


    //overload clears gfx
    public void UpdateGFX()
    {
        if (currentVeggie != null)
        {
            Destroy(currentVeggie);
        }
    }

}
