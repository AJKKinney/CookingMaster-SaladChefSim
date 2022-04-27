using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixture
{
    public readonly List<Vegetable> vegetables = new List<Vegetable>();

    //constructor
    public Mixture(Vegetable veggie)
    {
        this.AddVeggie(veggie);
    }

    //generate Mixture name
    public virtual string GetName()
    {
        string mixtureName = "";

        for(int i = 0; i < vegetables.Count; i++)
        {
            if(vegetables[i] != null)
            {
                mixtureName += vegetables[i].GetName() + " ";
            }
        }


        mixtureName += "Mixture";

        return mixtureName;
    }

    public void AddVeggie(Vegetable veggie)
    {
        vegetables.Add(veggie);
    }
}
