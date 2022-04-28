using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mini Corn
public class MiniCorn : Vegetable
{
    public void Awake()
    {
        chopTime = 0.25f;
    }

    public override string GetName()
    {
        return "Mini Corn";
    }

    public override int GetID()
    {
        return 2;
    }
}

