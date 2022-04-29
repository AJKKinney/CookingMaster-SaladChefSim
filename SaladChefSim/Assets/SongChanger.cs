using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongChanger : MonoBehaviour
{
    public AudioClip song;
    private bool started = false ;

    //call to change to song
    public void ChangeToSong()
    {
        MusicController.instance.ChangeSong(song);
    }
}
