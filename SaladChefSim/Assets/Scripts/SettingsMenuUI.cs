using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//controls the settings menu
public class SettingsMenuUI : MonoBehaviour
{

    public Slider musicSlider;
    public Slider sfxSlider;


    //update music volume
    public void SetMusicVolume()
    {
        AudioSettingsController.musicVolume = musicSlider.value;
        MusicController.instance.SetMusicVolume();
    }


    //update sfx volume
    public void SetSFXVolume()
    {
        AudioSettingsController.musicVolume = sfxSlider.value;
        SFXAudioController.instance.SetSFXVolume();
    }


    //delete save data
    public void DeleteSaveData()
    {
        SaveSystem.DeleteSave();
    }
}
