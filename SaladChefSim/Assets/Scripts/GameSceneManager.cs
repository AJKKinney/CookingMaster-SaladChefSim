using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;


//handles launching game and quitting
public class GameSceneManager : MonoBehaviour
{
    [Header("Scene Loading Settings")]
    public Image loadingPanel;
    public Slider loadingBar;
    public GameObject prompt;

    private bool awaitInput = false;

    AsyncOperation loading;

    // Update is called once per frame
    void Update()
    {

        //loading update loop
        if (loading != null)
        {
            //update loading bar progress
            float loadingProgress = loading.progress;
            //add 0.1 to account for scene activation
            loadingBar.value = loadingProgress + 0.1f;

            //activate scene on keypress
            if (loading.progress >= 0.9f)
            {
                if(awaitInput == true)
                {
                    prompt.SetActive(true);
                }

                if (Keyboard.current.anyKey.wasPressedThisFrame == true)
                {
                    loading.allowSceneActivation = true;
                }
            }
        }
    }

    //starts the async loading operation
    public void BeginLoadGame()
    {
        loadingPanel.gameObject.SetActive(true);
        loading = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene");

        //hide prompt until loaded
        prompt.SetActive(false);
        awaitInput = true;

        //hold the scene until activation
        loading.allowSceneActivation = false;
    }

    //Quits games
    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ReturnToMenu()
    {
        loadingPanel.gameObject.SetActive(true);
        //hide prompt until loaded
        prompt.SetActive(false);
        loading = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MenuScene");
    }
}
