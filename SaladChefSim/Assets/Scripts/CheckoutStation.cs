﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Checkout Station is the class that handles customers
//players bring salads to the checkout stations in order to serve the customers
//The checkout station also determines if a customer is waiting
[RequireComponent(typeof(CustomerGenerator))]
[RequireComponent(typeof(PickupGenerator))]
public class CheckoutStation : MonoBehaviour
{
    [HideInInspector]
    public bool customerWaiting = false;
    private float timer;
    readonly private float timerLow = 12f;
    readonly private float timerHigh = 40f;
    private float timeMod = 1f;
    readonly private float customerWaitPenalty = 0.5f;
    private float customerMaxWaitTime;
    [HideInInspector]
    public int[] desiredMixture;
    private CustomerGenerator customerGenerator;
    private PickupGenerator pickupGenerator;
    private bool paused = false;
    private bool[] playersWrong = new bool[2]  { false, false };

    [Header("Customers")]
    readonly private float baseWaitTime = 35f;
    readonly private float timePerVeg = 10f;
    public GameObject customerGFX;
    public bool startingCustomer = false;
    CustomerUI customerUI;

    [Header("Audio")]
    public AudioClip[] happySFX;
    public AudioClip[] angrySFX;
    public AudioClip newCustomerSFX;

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
        customerUI = GetComponentInChildren<CustomerUI>();
        pickupGenerator = GetComponent<PickupGenerator>();
    }

    private void Update()
    {
        if (paused == false)
        {
            //timing for new customers
            if (timer > 0)
            {
                timer -= Time.deltaTime * timeMod;
                //update HUD for Customer
                if (customerWaiting == true)
                {
                    customerUI.SetTimerUI(timer, customerMaxWaitTime);
                }
            }
            else if (customerWaiting == true && timeMod <= 1f)
            {
                customerGFX.SetActive(false);
                //reduce points for both players
                ScoreTracker.instance.AddPoints(ScoreTracker.instance.penaltyForLeaver);

                timer = Random.Range(timerLow, timerHigh);

                customerWaiting = false;
            }
            else if(customerWaiting == true && timeMod > 1f)
            {

                //reduce points for  players for angry customer
                if(playersWrong[0] && playersWrong[1])
                {
                    ScoreTracker.instance.AddPoints(ScoreTracker.instance.penaltyForLeaver * 2);
                }
                else if (playersWrong[0])
                {
                    ScoreTracker.instance.AddPoints(ScoreTracker.instance.penaltyForLeaver * 2, 1);
                }
                else if (playersWrong[1])
                {
                    ScoreTracker.instance.AddPoints(ScoreTracker.instance.penaltyForLeaver * 2, 2);
                }

                timeMod = 1f;
                ResetWrong();
                customerWaiting = false;
            }
            else
            {
                //generate customer desires and set time to wait based on number of desired veggies
                int numVeg;
                desiredMixture = customerGenerator.GenerateCustomer(out numVeg);
                customerUI.UpdateDesires(desiredMixture);

                //play new customer sfx if needed
                if (customerGFX.activeSelf != true)
                {
                    SFXAudioController.instance.PlaySFX(newCustomerSFX);
                    customerGFX.SetActive(true);
                }

                customerWaiting = true;
                timer = baseWaitTime + timePerVeg * numVeg;
                customerMaxWaitTime = timer;
            }
        }
    }




    //Checks the salad given to the customer to see if it matches their preferences.
    //returns true if the customer was served with more than 70% time remaining
    public bool ServeCustomer(Mixture salad, int player)
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

        //check mixture
        bool correctSalad = true;

        for(int i = 0; i < servedMixture.Length; i++)
        {
            //Debug.Log("served " + servedMixture[i].ToString());
            //Debug.Log("desired " + desiredMixture[i].ToString());
            if(servedMixture[i] != desiredMixture[i])
            {
                correctSalad = false;
            }
        }

        //serve mixture
        if (correctSalad == true)
        {
            //award points for correct dish
            Debug.Log("You Correctly Served the Customer " + salad.GetName());

            //play Happy SFX
            SFXAudioController.instance.PlaySFX(happySFX);


            //customer no longer waiting
            customerGFX.SetActive(false);
            customerWaiting = false;

            //award points to the player who served the food
            ScoreTracker.instance.AddPoints(ScoreTracker.instance.basePointsAwarded, player);
            ResetWrong();

            //powerup if >%70 time remaining
            if (timer >= customerMaxWaitTime * 0.7f)
            {
                Debug.Log("You generated a pickup!");
                pickupGenerator.GeneratePickup(player);
                return true;
            }
        }
        else
        {
            //increase tick speed for incorrect dish
            Debug.Log("You Incorrectly Served the Customer " + salad.GetName());

            //Play Angry SFX
            SFXAudioController.instance.PlaySFX(angrySFX);

            //update wrong player for penalty
            RegisterPlayerWrong(player);

            timeMod += customerWaitPenalty;
        }
        return false;
    }

    //suspends the update loop
    public void Pause()
    {
        paused = true;
    }

    //resumes the update loop
    public void Resume()
    {
        paused = false;
    }

    public void RegisterPlayerWrong(int player)
    {
        playersWrong[player - 1] = true;
    }

    public void ResetWrong()
    {
        for(int i = 0; i < playersWrong.Length; i++)
        {
            playersWrong[i] = false;
        }
    }
}
