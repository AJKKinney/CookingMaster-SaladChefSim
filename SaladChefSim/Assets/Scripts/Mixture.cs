using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixture : MonoBehaviour
{
    [HideInInspector]
    public bool salad;
    public readonly List<Vegetable> vegetables = new List<Vegetable>();

    //generate Mixture name
    public virtual string GetName()
    {
        string mixtureName = "";

        for(int i = 0; i < vegetables.Count - 1; i++)
        {
            if(vegetables[i] != null)
            {
                mixtureName += vegetables[i].GetName() + " ";
            }
        }

        if(salad == true)
        {
            mixtureName += "Salad";
        }
        else
        {
            mixtureName += "Mixture";
        }

        return mixtureName;
    }

    public void AddVeggie(Vegetable veggie)
    {
        vegetables.Add(veggie);
    }
}
