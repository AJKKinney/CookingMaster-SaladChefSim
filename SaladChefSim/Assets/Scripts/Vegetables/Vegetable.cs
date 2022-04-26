using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for vegetables
public class Vegetable : MonoBehaviour
{
    [HideInInspector]
    public bool chopped;
    public float chopTime { get; set; } = 1;


    public virtual string GetName()
    {
        return "Vegetable";
    }

    public virtual Vegetable Grab()
    {
        return this;
    }
}
