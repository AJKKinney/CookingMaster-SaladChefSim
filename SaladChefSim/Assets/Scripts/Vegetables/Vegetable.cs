using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for vegetables
public class Vegetable : MonoBehaviour
{
    public float chopTime { get; set; } = 1;

    //return the vegetables name as a string
    public virtual string GetName()
    {
        return "Vegetable";
    }

    //get the vegetables vegetableID
    public virtual int GetID()
    {
        return -1;
    }

    //the vegetable types
    public enum Ingredients
    {
        Lettuce,
        Spinach,
        MiniCorn,
        Tomato,
        RedCabbage,
        Radish
    }
}
