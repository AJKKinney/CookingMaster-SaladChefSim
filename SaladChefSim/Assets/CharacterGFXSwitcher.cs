using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGFXSwitcher : MonoBehaviour
{

    public PlayerAnimationManager playerAnimManager;

    // Start is called before the first frame update
    void Start()
    {

        GameObject character = GetChosenCharacter();
        if (character != null)
        {
            //Create Character GFX and assign player animator
            playerAnimManager.playerAnimator = Instantiate(character, transform).GetComponent<Animator>();
            playerAnimManager.armsAnimator = playerAnimManager.GetComponentInChildren<ArmsAnimator>();
        }

    }

    //return the chosen character
    public virtual GameObject GetChosenCharacter()
    {
        return CharacterSelectionController.chosenCharacterPrefabPlayerOne;
    }
}
