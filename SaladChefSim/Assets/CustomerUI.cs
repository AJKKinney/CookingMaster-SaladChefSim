using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CustomerUI : MonoBehaviour
{
    public Canvas customerHUD;
    public Slider timerUI;
    private CheckoutStation checkout;
    

    public void SetTimerUI(float timer, float maxtime)
    {
        timerUI.value = timer / maxtime;
    }

    public void DisableCustomerUI()
    {
        customerHUD.gameObject.SetActive(false);
    }
}
