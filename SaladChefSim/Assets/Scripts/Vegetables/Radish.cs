using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Radish
public class Radish : Vegetable
{
    public void Awake()
    {
        chopTime = 0.5f;
    }

    public override string GetName()
    {
        return "Radish";
    }
}
