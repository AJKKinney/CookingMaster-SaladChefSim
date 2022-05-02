using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The class that stores data for salad mixtures
public class Mixture
{
    public readonly List<Vegetable> vegetables = new List<Vegetable>();

    //constructor
    public Mixture(Vegetable veggie)
    {
        this.AddVeggie(veggie);
    }

    //generates a Mixture name as a string
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

    //Adds a vegetable to the mixture
    public void AddVeggie(Vegetable veggie)
    {
        vegetables.Add(veggie);
    }
}
