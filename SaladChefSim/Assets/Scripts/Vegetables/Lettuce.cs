using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lettuce
public class Lettuce : Vegetable
{
    public void Awake()
    {
        //set chop time for vegetable
        chopTime = 0.35f;
    }

    //return the vegetables name as a string
    public override string GetName()
    {
        return "Lettuce";
    }

    //get the vegetables vegetableID
    public override int GetID()
    {
        return (int) Ingredients.Lettuce;
    }

}
