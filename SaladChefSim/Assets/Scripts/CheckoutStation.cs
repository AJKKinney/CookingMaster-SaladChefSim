using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When You Have time Break this class up into single responsibilities - 5/2/2022 AK

[RequireComponent(typeof(CustomerGenerator))]
[RequireComponent(typeof(PickupGenerator))]

//The Checkout Station is the class that handles customers
//players bring salads to the checkout stations in order to serve the customers
//The checkout station also determines if a customer is waiting

public class CheckoutStation : MonoBehaviour
{
    [Header("Customers")]
    public GameObject customerGFX;
    public bool startingCustomer = false;


    [Header("Audio")]
    public AudioClip[] happySFX;
    public AudioClip[] angrySFX;
    public AudioClip newCustomerSFX;

    //stores the desired vegetable ids for a customer
    [HideInInspector]
    public int[] desiredMixture;
    //determines whether a customer is waiting for their meal or not
    [HideInInspector]
    public bool customerWaiting = false;

    //Pauses the timers when true
    private bool paused = false;
    //stores the info for when a player serves the wrong salad
    private bool[] playersWrong = new bool[2] { false, false };
    //customer timer
    private float timer;
    //the time modifier. Increase the time modifier to make the timer run faster e.g. when a customer is served the wrong salad
    private float timeMod = 1f;
    //stores the amount of time that a customer is willing to wait
    private float customerMaxWaitTime;
    //generates customer orders
    private CustomerGenerator customerGenerator;
    //generates pickups
    private PickupGenerator pickupGenerator;
    //shows the customers desired salad
    private CustomerUI customerUI;
    //sets the base amount of time that customers will wait for a salad
    readonly private float baseWaitTime = 35f;
    //sets the additional time a customer will wait per vegetable on their order
    readonly private float timePerVeg = 10f;
    //sets the shortest amount of time possible between the generation of new customers
    readonly private float timerLow = 12f;
    //sets the highest amount of time possible between the generation of new customers
    readonly private float timerHigh = 40f;
    //sets the amount the timeMod variable will increase by when a player serves the wrong salad
    readonly private float customerWaitPenalty = 0.5f;



    private void Awake()
    {
        //initialize
        customerGenerator = GetComponent<CustomerGenerator>();
        customerUI = GetComponentInChildren<CustomerUI>();
        pickupGenerator = GetComponent<PickupGenerator>();

        //set up
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
        //return if paused
        if (paused == true)
        {
            return;
        }


        //timing for customers
        //tick if timer is not 0
        if (timer > 0)
        {
            Tick();
        }
        //timeout if timer reaches 0 and a customer waits
        else if (customerWaiting == true)
        {
            CustomerTimeout();
        }
        //generate a new customer if the timer reaches 0 and no customer waits
        else
        {
            NewCustomer();
        }
    }


    //call to have a waiting customer leave
    private void CustomerTimeout()
    {

        //reduce points based on player's serving
        if (playersWrong[0] && playersWrong[1])
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
        else
        {
            //reduce points for both players
            ScoreTracker.instance.AddPoints(ScoreTracker.instance.penaltyForLeaver);
        }

        //reset customer
        customerGFX.SetActive(false);
        timeMod = 1f;
        ResetWrong();
        customerWaiting = false;

        Debug.Log("A Customer Left");

        //set timer
        timer = Random.Range(timerLow, timerHigh);
    }


    //generate customer desires and set time to wait based on number of desired veggies
    private void NewCustomer()
    {
        //generate desires
        desiredMixture = customerGenerator.GenerateCustomerOrder(out int numVeg);
        customerUI.UpdateDesires(desiredMixture);

        //play new customer sfx if needed
        if (customerGFX.activeSelf != true)
        {
            SFXAudioController.instance.PlaySFX(newCustomerSFX);
            customerGFX.SetActive(true);
        }

        //setup timer
        customerWaiting = true;
        customerMaxWaitTime = baseWaitTime + (timePerVeg * numVeg);

        Debug.Log("A new Customer Arrived");

        timer = customerMaxWaitTime;
    }


    //Checks the salad given to the customer to see if it matches their preferences.
    //returns true if the customer was served with more than 70% time remaining
    public bool ServeCustomer(Mixture salad, int player)
    {
        int[] servedMixture = new int[6];

        //check if matches desires
        for (int i = 0; i < salad.vegetables.Count; i++)
        {
            if (salad.vegetables[i].GetType() == typeof(Lettuce))
            {
                servedMixture[0] += 1;
            }
            else if (salad.vegetables[i].GetType() == typeof(Spinach))
            {
                servedMixture[1] += 1;
            }
            else if (salad.vegetables[i].GetType() == typeof(MiniCorn))
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

        for (int i = 0; i < servedMixture.Length; i++)
        {
            //Debug.Log("served " + servedMixture[i].ToString());
            //Debug.Log("desired " + desiredMixture[i].ToString());
            if (servedMixture[i] != desiredMixture[i])
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


    //updates the customer timer
    private void Tick()
    {
        //update HUD for Customer
        if (customerWaiting == true)
        {
            customerUI.SetTimerUI(timer, customerMaxWaitTime);
        }

        //update timer
        timer -= Time.deltaTime * timeMod;
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


    //register a player for serving the wrong salad
    public void RegisterPlayerWrong(int player)
    {
        playersWrong[player - 1] = true;
    }


    //reset all information on players having served the wrong salad
    public void ResetWrong()
    {
        for(int i = 0; i < playersWrong.Length; i++)
        {
            playersWrong[i] = false;
        }
    }
}
