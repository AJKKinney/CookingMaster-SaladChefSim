using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//singleton you attach to the music source to play music to it

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    private AudioSource songSource;
    public static MusicController instance;


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
        songSource = GetComponent<AudioSource>();
    }


    private void Start()
    {
        SetMusicVolume();
    }


    //changes the song
    public void ChangeSong(AudioClip song)
    {
        songSource.Stop();
        songSource.clip = song;
        songSource.Play();
    }


    //starts the current song if stopped
    public void StartSong()
    {
        if (songSource.isPlaying == false)
        {
            songSource.Play();
        }
    }


    //stops the music that is currently playing
    public void StopMusic()
    {
        songSource.Stop();
    }


    //sets the music volume to the saved settings
    public void SetMusicVolume()
    {
        songSource.volume = AudioSettingsController.musicVolume;
    }

}
