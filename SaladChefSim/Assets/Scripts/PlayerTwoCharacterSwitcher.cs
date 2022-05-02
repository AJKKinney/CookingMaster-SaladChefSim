using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//loads the chosen character from select screen and sets it up for animation for player two
public class PlayerTwoCharacterSwitcher : CharacterGFXSwitcher
{
    //return the chosen character
    public override GameObject GetChosenCharacter()
    {
        return CharacterSelectionController.chosenCharacterPrefabPlayerTwo;
    }
}
