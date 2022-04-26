using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Red Cabbage
public class RedCabbage : Vegetable
{
    public void Awake()
    {
        chopTime = 0.75f;
    }

    public override string GetName()
    {
        return "Red Cabbage";
    }
}