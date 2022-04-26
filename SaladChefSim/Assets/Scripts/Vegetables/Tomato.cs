using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tomato
public class Tomato : Vegetable
{
    public void Awake()
    {
        chopTime = 0.6f;
    }

    public override string GetName()
    {
        return "Tomato";
    }
}
