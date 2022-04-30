using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{

    public Slider musicSlider;
    public Slider sfxSlider;


    public void SetMusicVolume()
    {
        AudioSettingsController.musicVolume = musicSlider.value;
        MusicController.instance.SetMusicVolume();
    }


    public void SetSFXVolume()
    {
        AudioSettingsController.musicVolume = sfxSlider.value;
        SFXAudioController.instance.SetSFXVolume();
    }
}
