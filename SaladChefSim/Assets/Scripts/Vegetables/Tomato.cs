using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tomato
public class Tomato : Vegetable
{
    public void Awake()
    {
        //set chop time for vegetable
        chopTime = 0.6f;
    }

    //return the vegetables name as a string
    public override string GetName()
    {
        return "Tomato";
    }

    //get the vegetables vegetableID
    public override int GetID()
    {
        return (int) Ingredients.Tomato;
    }
}
