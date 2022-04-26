using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lettuce : Vegetable
{
    public void Awake()
    {
        chopTime = 0.35f;
    }

    public override string GetName()
    {
        return "Lettuce";
    }
}
