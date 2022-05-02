using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Red Cabbage
public class RedCabbage : Vegetable
{
    public void Awake()
    {
        //set chop time for vegetable
        chopTime = 0.75f;
    }

    //return the vegetables name as a string
    public override string GetName()
    {
        return "Red Cabbage";
    }

    //get the vegetables vegetableID
    public override int GetID()
    {
        return (int)Ingredients.RedCabbage;
    }
}