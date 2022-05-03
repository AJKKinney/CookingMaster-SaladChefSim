using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains and plays chopping sfx when the attached game object is active
public class ChoppingSFXController : MonoBehaviour
{
    public AudioClip[] chopSFX;

    private float timer = 0f;
    readonly private float timeBetween = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            SFXAudioController.instance.PlaySFX(chopSFX);
            timer = timeBetween;
        }
    }
}
