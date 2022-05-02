using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Radish
public class Radish : Vegetable
{
    public void Awake()
    {
        //set chop time for vegetable
        chopTime = 0.5f;
    }

    //return the vegetables name as a string
    public override string GetName()
    {
        return "Radish";
    }

    //get the vegetables vegetableID
    public override int GetID()
    {
        return (int) Ingredients.Radish;
    }
}
