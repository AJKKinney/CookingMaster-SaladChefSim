using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controls the salad display gfx
public class SaladGFXController : MonoBehaviour
{
    [Header("GFX Objects")]
    public GameObject saladGFXParent;
    public GameObject[] veggies;

    public void displaySalad(Mixture salad)
    {
        saladGFXParent.SetActive(true);

        //check for types of veggies
        for (int i = 0; i < salad.vegetables.Count; i++)
        {
            if (salad.vegetables[i].GetType() == typeof(Lettuce))
            {
                veggies[(int) Vegetable.Ingredients.Lettuce].SetActive(true);
            }
            else if (salad.vegetables[i].GetType() == typeof(Spinach))
            {
                veggies[(int)Vegetable.Ingredients.Spinach].SetActive(true);
            }
            else if (salad.vegetables[i].GetType() == typeof(MiniCorn))
            {
                veggies[(int)Vegetable.Ingredients.MiniCorn].SetActive(true);
            }
            else if (salad.vegetables[i].GetType() == typeof(Tomato))
            {
                veggies[(int)Vegetable.Ingredients.Tomato].SetActive(true);
            }
            else if (salad.vegetables[i].GetType() == typeof(RedCabbage))
            {
                veggies[(int)Vegetable.Ingredients.RedCabbage].SetActive(true);
            }
            else if (salad.vegetables[i].GetType() == typeof(Radish))
            {
                veggies[(int)Vegetable.Ingredients.Radish].SetActive(true);
            }
        }
    }


    //hides the gfx objects
    public void HideSalad()
    {
        for (int i = 0; i < veggies.Length; i++)
        {
            veggies[i].SetActive(false);
        }

        saladGFXParent.SetActive(false);
    }

}
