using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{

    public AudioClip[] footsteps;
    public void Step()
    {
        SFXAudioController.instance.PlaySFX(footsteps);
    }
}
