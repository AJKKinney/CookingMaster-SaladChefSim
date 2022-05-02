using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//controls the timer ui for chopping veggies
public class PlayerChoppingHUD : MonoBehaviour
{
    [Header("Player UI Slider")]
    public Slider choppingSlider;

    private float startTime;
    private float timer;

    private void Update()
    {
        if(timer > 0)
        {
            choppingSlider.value = timer / startTime;
            timer -= Time.deltaTime;
        }
        else if(choppingSlider.gameObject.activeSelf == true)
        {
            choppingSlider.gameObject.SetActive(false);
        }
    }

    //Starts the chopping veggies timer
    public void ChopVeggies(float length)
    {
        startTime = length;
        timer = startTime;
        choppingSlider.gameObject.SetActive(true);
    }
}
