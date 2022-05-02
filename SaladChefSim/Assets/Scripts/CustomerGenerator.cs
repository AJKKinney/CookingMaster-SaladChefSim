using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for generating customer preferences
public class CustomerGenerator : MonoBehaviour
{
    readonly private int minIngredients = 2;
    readonly private int maxIngredients = 6;


    //generates a new customer order returns int[] representing the order
    public int[] GenerateCustomerOrder(out int numberOfVegetables)
    {
        //int array representing number of veggies in each type index = vegetableID, value = numberOfVeggies
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
}
