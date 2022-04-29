using System.Collections;
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
    public GameObject playerTwoIndicator;
    public TextMeshProUGUI playerTwoUserNameUI;
    public GameSceneManager sceneManager;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Enable();
    }

    private void Update()
    {
        //player 1

        //char select
        if (playerOneSelected != true)
        {
            if (controls.PlayerOneActions.Movement.ReadValue<Vector2>().x > 0 && controls.PlayerOneActions.Movement.WasPressedThisFrame())
            {
                if(playerOneIndex < characterPrefabs.Length - 1)
                {
                    playerOneIndex += 1;
                }
            }
            else if (controls.PlayerOneActions.Movement.ReadValue<Vector2>().x < 0 && controls.PlayerOneActions.Movement.WasPressedThisFrame())
            {
                if (playerOneIndex > 0)
                {
                    playerOneIndex -= 1;
                }
            }

            playerOneIndicator.transform.position = transform.GetChild(playerOneIndex).transform.position;

            if (controls.PlayerOneActions.Interact.WasPressedThisFrame())
            {
                CharacterSelectionController.chosenCharacterPrefabPlayerOne = characterPrefabs[playerOneIndex];
                playerOneIndex = 0;
                playerOneUserNameUI.gameObject.SetActive(true);
                playerOneSelected = true;
            }
        }
        //username select
        else if (playerOneName.Length < 3)
        {
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
            }

            //updatUI
            playerOneUserNameUI.text = playerOneName + alpha[playerOneIndex];

            if (controls.PlayerOneActions.Interact.WasPressedThisFrame())
            {
                playerOneName += alpha[playerOneIndex];

                if(playerOneName.Length == 3)
                {
                    CharacterSelectionController.playerOneInitials = playerOneName;
                    //activate check mark when ready
                    playerOneUserNameUI.transform.GetChild(0).gameObject.SetActive(true);


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
            if (controls.PlayerTwoActions.Movement.ReadValue<Vector2>().x > 0 && controls.PlayerTwoActions.Movement.WasPressedThisFrame())
            {
                if (playerTwoIndex < characterPrefabs.Length - 1)
                {
                    playerTwoIndex += 1;
                }
            }
            else if (controls.PlayerTwoActions.Movement.ReadValue<Vector2>().x < 0 && controls.PlayerTwoActions.Movement.WasPressedThisFrame())
            {
                if (playerTwoIndex > 0)
                {
                    playerTwoIndex -= 1;
                }
            }

            playerTwoIndicator.transform.position = transform.GetChild(playerTwoIndex).transform.position;

            if (controls.PlayerTwoActions.Interact.WasPressedThisFrame())
            {
                CharacterSelectionController.chosenCharacterPrefabPlayerTwo = characterPrefabs[playerTwoIndex];
                playerTwoIndex = 0;
                playerTwoUserNameUI.gameObject.SetActive(true);
                playerTwoSelected = true;
            }
        }
        //username select
        else if (playerTwoName.Length < 3)
        {
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
            }

            //updatUI
            playerTwoUserNameUI.text = playerTwoName + alpha[playerTwoIndex];

            if (controls.PlayerTwoActions.Interact.WasPressedThisFrame())
            {
                playerTwoName += alpha[playerTwoIndex];

                if (playerTwoName.Length == 3)
                {
                    CharacterSelectionController.playerTwoInitials = playerTwoName;
                    //activate check mark when ready
                    playerTwoUserNameUI.transform.GetChild(0).gameObject.SetActive(true);

                    //start if both are ready
                    if(playerOneName.Length == 3)
                    {
                        sceneManager.BeginLoadGame();
                    }
                }
            }
        }


    }
}
