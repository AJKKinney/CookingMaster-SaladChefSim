using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//makes the vegetable stored on the plate visible
public class PlateGFXController : MonoBehaviour
{
    [HideInInspector]
    public GameObject currentVeggie;
    public GameObject[] veggieGFX;

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
