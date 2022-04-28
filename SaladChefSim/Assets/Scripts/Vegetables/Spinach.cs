using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinach : Vegetable
{
    public void Awake()
    {
        chopTime = 0.3f;
    }

    public override string GetName()
    {
        return "Spinach";
    }


    public override int GetID()
    {
        return 1;
    }
}
