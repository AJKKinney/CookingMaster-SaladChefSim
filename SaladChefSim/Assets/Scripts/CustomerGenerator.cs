using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for generating customer preferences
public class CustomerGenerator : MonoBehaviour
{
    readonly private int minIngredients = 2;
    readonly private int maxIngredients = 6;

    public int[] GenerateCustomer(out int numberOfVegetables)
    {
        int[] customerOrder = new int[6];

        int numIngredients = Mathf.RoundToInt(Random.Range(minIngredients, maxIngredients));

        for(int i = 0; i < numIngredients; i++)
        {
            int ingredient = Mathf.RoundToInt(Random.Range(0, 6));
            customerOrder[ingredient] += 1;
        }

        numberOfVegetables = numIngredients;
        return customerOrder;
    }

    public enum Ingredients
    {
        Lettuce,
        Spinach,
        MiniCorn,
        Tomato,
        RedCabbage,
        Radish
    }
}
