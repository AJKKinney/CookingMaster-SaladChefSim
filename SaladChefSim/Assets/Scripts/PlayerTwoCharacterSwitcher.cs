using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoCharacterSwitcher : CharacterGFXSwitcher
{
    //return the chosen character
    public override GameObject GetChosenCharacter()
    {
        return CharacterSelectionController.chosenCharacterPrefabPlayerTwo;
    }
}
