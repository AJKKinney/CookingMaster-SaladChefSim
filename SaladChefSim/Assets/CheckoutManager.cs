using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutManager : MonoBehaviour
{
    public CheckoutStation[] checkouts;


    private void Awake()
    {
        checkouts = GetComponentsInChildren<CheckoutStation>();
    }


    //calls the pause function on all checkout stations in the children

    public void PauseCheckouts()
    {
        for(int i = 0; i < checkouts.Length; i++)
        {
            checkouts[i].Pause();
        }
    }

    //calls the resume function on all checkout stations in the children

    public void ResumeCheckouts()
    {
        for (int i = 0; i < checkouts.Length; i++)
        {
            checkouts[i].Resume();
        }
    }
}
