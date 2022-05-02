using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//stores and plays the sfx for gaining and losing points
public class GameSFXController : MonoBehaviour
{
    [Header("Score SFX")]
    public AudioClip gainPoint;
    public AudioClip losePoint;
    

    //plays the gain point sfx
    public void PlayScoreSFX()
    {
        SFXAudioController.instance.PlaySFX(gainPoint);
    }

    //plays the lose point sfx
    public void PlayPenaltySFX()
    {
        SFXAudioController.instance.PlaySFX(losePoint);
    }
}
