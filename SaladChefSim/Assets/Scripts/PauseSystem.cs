using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class controls the pause system of the game
public class PauseSystem : MonoBehaviour
{
    public GameObject pauseScreen;

    public CheckoutPauseManager checkoutManager;

    private PlayerControls controls;
    private GameTimer timer;
    private bool paused = false;


    private void Awake()
    {
        //initialize
        controls = new PlayerControls();
        controls.SystemActions.Enable();
        timer = GetComponent<GameTimer>();
    }

    //pauses when player presses escape
    private void Update()
    {
        if(controls.SystemActions.Pause.WasPressedThisFrame())
        {
            paused = !paused;
            TogglePause(paused);
            PauseUI(paused);
        }
    }

    //pause the game
    private void TogglePause(bool pause)
    {
        if (pause)
        {
            timer.Pause();
            checkoutManager.PauseCheckouts();
        }
        else
        {
            timer.Unpause();
            checkoutManager.ResumeCheckouts();
        }
    }

    private void PauseUI(bool enable)
    {
        if(enable == true)
        {
            pauseScreen.SetActive(true);
        }
        else
        {
            pauseScreen.SetActive(false);
        }
    }

}
