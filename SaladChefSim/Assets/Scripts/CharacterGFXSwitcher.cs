using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//loads the chosen character from select screen and sets it up for animation
public class CharacterGFXSwitcher : MonoBehaviour
{
    [Header("Animation")]
    public PlayerAnimationManager playerAnimationManager;


    void Start()
    {
        GameObject character = GetChosenCharacter();

        if (character != null)
        {
            //Create Character GFX and assign player animator
            playerAnimationManager.playerAnimator = Instantiate(character, transform).GetComponent<Animator>();
            playerAnimationManager.armsAnimator = playerAnimationManager.GetComponentInChildren<ArmsAnimator>();
        }

    }


    //return the chosen character
    public virtual GameObject GetChosenCharacter()
    {
        return CharacterSelectionController.chosenCharacterPrefabPlayerOne;
    }
}
