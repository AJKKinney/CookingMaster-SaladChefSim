using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

//singleton you attach to the sfx source to play sfx to it
public class SFXAudioController : MonoBehaviour
{
    public static SFXAudioController instance;

    private AudioSource sfxSource;

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

    private void Start()
    {
        SetSFXVolume();
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

    //sets volume
    public void SetSFXVolume()
    {
        sfxSource.volume = AudioSettingsController.sfxVolume;
    }
}
