using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//changes the song to the song set in inspector
public class SongChanger : MonoBehaviour
{
    public AudioClip song;

    //call to change to song
    public void ChangeToSong()
    {
        MusicController.instance.ChangeSong(song);
    }
}
