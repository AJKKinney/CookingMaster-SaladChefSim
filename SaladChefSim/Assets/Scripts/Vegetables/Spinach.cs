using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinach : Vegetable
{
    public void Awake()
    {
        //set chop time for vegetable
        chopTime = 0.3f;
    }

    //return the vegetables name as a string
    public override string GetName()
    {
        return "Spinach";
    }

    //get the vegetables vegetableID
    public override int GetID()
    {
        return (int) Ingredients.Spinach;
    }
}
