using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSFXController : MonoBehaviour
{
    public AudioClip gainPoint;
    public AudioClip losePoint;

    public void PlayScoreSFX()
    {
        SFXAudioController.instance.PlaySFX(gainPoint);
    }

    public void PlayPenaltySFX()
    {
        SFXAudioController.instance.PlaySFX(losePoint);
    }
}
