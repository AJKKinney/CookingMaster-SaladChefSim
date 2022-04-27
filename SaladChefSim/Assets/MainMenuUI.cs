using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene Loading Settings")]
    public Image loadingPanel;
    public Slider loadingBar;

    private bool launch = false;
    AsyncOperation loading;

    // Update is called once per frame
    void Update()
    {
        //loads the game
        if (launch)
        {
            BeginLoad();
        }

        //loading update loop
        if (loading != null)
        {
            //update loading bar progress
            float loadingProgress = loading.progress;
            //add 0.1 to account for scene activation
            loadingBar.value = loadingProgress + 0.1f;

            //activate scene on keypress
            if (loading.progress >= 0.9f && Keyboard.current.anyKey.wasPressedThisFrame == true)
            {
                loading.allowSceneActivation = true;
            }
        }
    }

    //Launch Game
    public void StartGame()
    {
        launch = true;
    }

    //starts the async loading operation
    public void BeginLoad()
    {
        loadingPanel.gameObject.SetActive(true);
        loading = SceneManager.LoadSceneAsync("MainScene");

        //hold the scene until activation
        loading.allowSceneActivation = false;
        launch = false;
    }
}
