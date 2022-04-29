using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXAudioController : MonoBehaviour
{
    private AudioSource sfxSource;
    public static SFXAudioController instance;

    // Start is called before the first frame update
    void Awake()
    {
        //singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        //get source
        sfxSource = GetComponent<AudioSource>();
    }

    //play single sfx
    public void PlaySFX(AudioClip sfx)
    {
        sfxSource.PlayOneShot(sfx);
    }

    //play random sfx from array
    public void PlaySFX(AudioClip[] sfx)
    {
        int index = Random.Range(0, sfx.Length);
        sfxSource.PlayOneShot(sfx[index]);
    }
}