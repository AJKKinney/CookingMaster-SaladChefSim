using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mini Corn
public class MiniCorn : Vegetable
{
    public void Awake()
    {
        //set chop time for vegetable
        chopTime = 0.25f;
    }

    //return the vegetables name as a string
    public override string GetName()
    {
        return "Mini Corn";
    }

    //get the vegetables vegetableID
    public override int GetID()
    {
        return (int) Ingredients.MiniCorn;
    }
}

