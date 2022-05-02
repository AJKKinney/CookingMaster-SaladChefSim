using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//holds and plays sfx for player interactions
public class InteractionSFXController : MonoBehaviour
{
    public AudioClip[] pickUp;
    public AudioClip[] putDown;

    public void PlayPickUpSFX()
    {
        SFXAudioController.instance.PlaySFX(pickUp);
    }

    public void PlayPutDownSFX()
    {
        SFXAudioController.instance.PlaySFX(putDown);
    }
}
