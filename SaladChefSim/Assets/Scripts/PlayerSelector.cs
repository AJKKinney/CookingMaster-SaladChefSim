﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Controls the player character selection and name entry
public class PlayerSelector : MonoBehaviour
{
    private PlayerControls controls;
    private int playerOneIndex = 0;
    private bool playerOneSelected;
    private string playerOneName = "";
    private int playerTwoIndex = 0;
    private bool playerTwoSelected;
    private string playerTwoName = "";
    private char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public GameObject[] characterPrefabs;

    public GameObject playerOneIndicator;
    public TextMeshProUGUI playerOneUserNameUI;
    public TextMeshProUGUI playerOnePrompt;
    public GameObject playerTwoIndicator;
    public TextMeshProUGUI playerTwoUserNameUI;
    public GameObject playerTwoPromptOne;
    public GameObject playerTwoPromptTwo;
    public GameSceneManager sceneManager;
    public GameObject playerOneCharAnchor;
    public GameObject playerTwoCharAnchor;

    public SFXControllerUI sfx;


    private void Awake()
    {
        controls = new PlayerControls();

        controls.Enable();
    }

    private void Update()
    {
        //Return on Cancel
        if(controls.PlayerOneActions.Cancel.WasPressedThisFrame() || controls.PlayerTwoActions.Cancel.WasPressedThisFrame())
        {
            sceneManager.ReturnToMenu();
        }

        //player 1

        //char select
        if (playerOneSelected != true)
        {
            //display controls
            playerOnePrompt.text = "Swap: a & d\nSelect: c";

            if (controls.PlayerOneActions.Movement.ReadValue<Vector2>().x > 0 && controls.PlayerOneActions.Movement.WasPressedThisFrame())
            {
                if(playerOneIndex < characterPrefabs.Length - 1)
                {
                    playerOneIndex += 1;

                    //set the correct character active
                    playerOneCharAnchor.transform.GetChild(playerOneIndex - 1).gameObject.SetActive(false);
                    playerOneCharAnchor.transform.GetChild(playerOneIndex).gameObject.SetActive(true);
                    sfx.PlaySoftUIBlip();
                }
            }
            else if (controls.PlayerOneActions.Movement.ReadValue<Vector2>().x < 0 && controls.PlayerOneActions.Movement.WasPressedThisFrame())
            {
                if (playerOneIndex > 0)
                {
                    playerOneIndex -= 1;

                    //set the correct character active
                    playerOneCharAnchor.transform.GetChild(playerOneIndex + 1).gameObject.SetActive(false);
                    playerOneCharAnchor.transform.GetChild(playerOneIndex).gameObject.SetActive(true);
                    sfx.PlaySoftUIBlip();
                }
            }

            //update selector UI
            playerOneIndicator.transform.position = transform.GetChild(playerOneIndex).transform.position;


            if (controls.PlayerOneActions.Interact.WasPressedThisFrame())
            {

                CharacterSelectionController.chosenCharacterPrefabPlayerOne = characterPrefabs[playerOneIndex];
                playerOneIndex = 0;
                playerOneUserNameUI.gameObject.SetActive(true);
                playerOneSelected = true;
                sfx.PlayUIBlip();
            }
        }
        //username select
        else if (playerOneName.Length < 3)
        {
            //display controls
            playerOnePrompt.text = "Swap: w & s\nSelect: c";

            if (controls.PlayerOneActions.Movement.ReadValue<Vector2>().y > 0 && controls.PlayerOneActions.Movement.WasPressedThisFrame())
            {
                if (playerOneIndex < alpha.Length - 1)
                {
                    playerOneIndex += 1;
                }
                else
                {
                    playerOneIndex = 0;
                }
                sfx.PlaySoftUIBlip();
            }
            else if (controls.PlayerOneActions.Movement.ReadValue<Vector2>().y < 0 && controls.PlayerOneActions.Movement.WasPressedThisFrame())
            {
                if (playerOneIndex > 0)
                {
                    playerOneIndex -= 1;
                }
                else
                {
                    playerOneIndex = alpha.Length - 1;
                }
                sfx.PlaySoftUIBlip();
            }

            //updatUI
            playerOneUserNameUI.text = playerOneName + alpha[playerOneIndex];

            if (controls.PlayerOneActions.Interact.WasPressedThisFrame())
            {
                playerOneName += alpha[playerOneIndex];

                sfx.PlayUIBlip();

                if (playerOneName.Length == 3)
                {
                    CharacterSelectionController.playerOneInitials = playerOneName;
                    //activate check mark when ready
                    playerOneUserNameUI.transform.GetChild(0).gameObject.SetActive(true);
                    //clear controls
                    playerOnePrompt.text = "";

                    //start if both are ready
                    if (playerTwoName.Length == 3)
                    {
                        sceneManager.BeginLoadGame();
                    }
                }
            }
        }

        //player 2

        //char select
        if (playerTwoSelected != true)
        {
            //displayControls
            if (playerTwoPromptOne.activeSelf == false)
            {
                playerTwoPromptOne.SetActive(true);
                playerTwoPromptTwo.SetActive(false);
            }

            if (controls.PlayerTwoActions.Movement.ReadValue<Vector2>().x > 0 && controls.PlayerTwoActions.Movement.WasPressedThisFrame())
            {
                if (playerTwoIndex < characterPrefabs.Length - 1)
                {
                    playerTwoIndex += 1;

                    //set the correct character active
                    playerTwoCharAnchor.transform.GetChild(playerTwoIndex - 1).gameObject.SetActive(false);
                    playerTwoCharAnchor.transform.GetChild(playerTwoIndex).gameObject.SetActive(true);
                    sfx.PlaySoftUIBlip();
                }
            }
            else if (controls.PlayerTwoActions.Movement.ReadValue<Vector2>().x < 0 && controls.PlayerTwoActions.Movement.WasPressedThisFrame())
            {
                if (playerTwoIndex > 0)
                {
                    playerTwoIndex -= 1;

                    //set the correct character active
                    playerTwoCharAnchor.transform.GetChild(playerTwoIndex + 1).gameObject.SetActive(false);
                    playerTwoCharAnchor.transform.GetChild(playerTwoIndex).gameObject.SetActive(true);
                    sfx.PlaySoftUIBlip();
                }
            }

            //update indicator ui
            playerTwoIndicator.transform.position = transform.GetChild(playerTwoIndex).transform.position;

            if (controls.PlayerTwoActions.Interact.WasPressedThisFrame())
            {
                CharacterSelectionController.chosenCharacterPrefabPlayerTwo = characterPrefabs[playerTwoIndex];
                playerTwoIndex = 0;
                playerTwoUserNameUI.gameObject.SetActive(true);
                playerTwoSelected = true;
                sfx.PlayUIBlip();
            }
        }
        //username select
        else if (playerTwoName.Length < 3)
        {
            //display controls
            if (playerTwoPromptTwo.activeSelf == false)
            {
                playerTwoPromptOne.SetActive(false);
                playerTwoPromptTwo.SetActive(true);
                sfx.PlaySoftUIBlip();
            }

            if (controls.PlayerTwoActions.Movement.ReadValue<Vector2>().y > 0 && controls.PlayerTwoActions.Movement.WasPressedThisFrame())
            {
                if (playerTwoIndex < alpha.Length - 1)
                {
                    playerTwoIndex += 1;
                }
                else
                {
                    playerTwoIndex = 0;
                }
                sfx.PlaySoftUIBlip();
            }
            else if (controls.PlayerTwoActions.Movement.ReadValue<Vector2>().y < 0 && controls.PlayerTwoActions.Movement.WasPressedThisFrame())
            {
                if (playerTwoIndex > 0)
                {
                    playerTwoIndex -= 1;
                }
                else
                {
                    playerTwoIndex = alpha.Length - 1;
                }
                sfx.PlaySoftUIBlip();
            }

            //updatUI
            playerTwoUserNameUI.text = playerTwoName + alpha[playerTwoIndex];

            if (controls.PlayerTwoActions.Interact.WasPressedThisFrame())
            {
                playerTwoName += alpha[playerTwoIndex];

                sfx.PlayUIBlip();

                if (playerTwoName.Length == 3)
                {
                    CharacterSelectionController.playerTwoInitials = playerTwoName;
                    //activate check mark when ready
                    playerTwoUserNameUI.transform.GetChild(0).gameObject.SetActive(true);
                    //disable prompts
                    playerTwoPromptOne.transform.parent.gameObject.SetActive(false);

                    //start if both are ready
                    if (playerOneName.Length == 3)
                    {
                        sceneManager.BeginLoadGame();
                    }
                }
            }
        }


    }
}