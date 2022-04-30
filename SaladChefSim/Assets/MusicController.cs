using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    private AudioSource songSource;
    public static MusicController instance;

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
        songSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetMusicVolume();
    }
    public void ChangeSong(AudioClip song)
    {
        songSource.Stop();
        songSource.clip = song;
        songSource.Play();
    }

    public void StartSong()
    {
        if (songSource.isPlaying == false)
        {
            songSource.Play();
        }
    }

    public void StopMusic()
    {
        songSource.Stop();
    }

    public void SetMusicVolume()
    {
        songSource.volume = AudioSettingsController.musicVolume;
    }

}
