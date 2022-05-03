using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//holds and plays vocal stabs of character names for character selection
public class PlayerSelectionSFXController : MonoBehaviour
{
    [Header("Character Name Vocal Stabs")]
    public AudioClip[] banana;
    public AudioClip[] toast;
    public AudioClip[] cookie;

    public void PlayCharacterNameSFX(int characterIndex)
    {
        //play the correct character sfx
        switch (characterIndex)
        {
            case 0:
                SFXAudioController.instance.PlaySFX(banana);
                break;
            case 1:
                SFXAudioController.instance.PlaySFX(toast);
                break;
            case 2:
                SFXAudioController.instance.PlaySFX(cookie);
                break;
            default:
                break;
        }
    }
}
