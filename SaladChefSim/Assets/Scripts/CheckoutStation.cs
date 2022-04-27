using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Checkout Station is the class that handles customers
//players bring salads to the checkout stations in order to serve the customers
//The checkout station also determines if a customer is waiting
[RequireComponent(typeof(CustomerGenerator))]
public class CheckoutStation : MonoBehaviour
{
    [HideInInspector]
    public bool customerWaiting = false;
    private float timer;
    readonly private float timerLow = 12f;
    readonly private float timerHigh = 40f;
    [HideInInspector]
    public int[] desiredMixture;
    private CustomerGenerator customerGenerator;


    [Header("Customers")]
    public GameObject customerGFX;
    public bool startingCustomer = false;



    private void Awake()
    {
        customerGenerator = GetComponent<CustomerGenerator>();
        if (startingCustomer == true)
        {
            timer = 0;
        }
        else
        {
            timer = Random.Range(timerLow, timerHigh);
        }
    }

    private void Update()
    {
        //timing for new customers
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(customerWaiting == true)
        {
            customerGFX.SetActive(false);
            customerWaiting = false;
            timer = Random.Range(timerLow, timerHigh);
        }
        else
        {
            desiredMixture = customerGenerator.GenerateCustomer();
            customerGFX.SetActive(true);
            customerWaiting = true;
            timer = Random.Range(timerLow, timerHigh);
        }
    }


    //Checks the salad given to the customer to see if it matches their preferences.
    public bool ServeCustomer(Mixture salad)
    {
        int[] servedMixture = new int[6];

        //check if matches desires
        for(int i = 0; i < salad.vegetables.Count; i++)
        {
            if (salad.vegetables[i].GetType() == typeof(Lettuce))
            {
                servedMixture[0] += 1;
            }
            else if(salad.vegetables[i].GetType() == typeof(Spinach))
            {
                servedMixture[1] += 1;
            }
            else if(salad.vegetables[i].GetType() == typeof(MiniCorn))
            {
                servedMixture[2] += 1;
            }
            else if (salad.vegetables[i].GetType() == typeof(Tomato))
            {
                servedMixture[3] += 1;
            }
            else if (salad.vegetables[i].GetType() == typeof(RedCabbage))
            {
                servedMixture[4] += 1;
            }
            else if (salad.vegetables[i].GetType() == typeof(Radish))
            {
                servedMixture[5] += 1;
            }
        }


        if(servedMixture == desiredMixture)
        {
            Debug.Log("You Correctly Served the Customer " + salad.GetName());
            customerGFX.SetActive(false);
            return true;
        }
        else
        {
            Debug.Log("You Incorrectly Served the Customer " + salad.GetName());
            customerGFX.SetActive(false);
            return false;
        }
    }
}
