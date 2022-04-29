using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGFXSwitcher : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        GameObject character = GetChosenCharacter();
        if (character != null)
        {
            Instantiate(character, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //return the chosen character
    public virtual GameObject GetChosenCharacter()
    {
        return CharacterSelectionController.chosenCharacaterPrefabPlayerOne;
    }
}
