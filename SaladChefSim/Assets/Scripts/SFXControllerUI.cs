using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControllerUI : MonoBehaviour
{

    public AudioClip[] UIBlips;
    public AudioClip[] softBlips;

    public void PlayUIBlip()
    {
        SFXAudioController.instance.PlaySFX(UIBlips);
    }

    public void PlaySoftUIBlip()
    {
        SFXAudioController.instance.PlaySFX(softBlips);
    }
}
