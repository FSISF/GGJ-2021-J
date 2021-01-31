using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelController : MonoBehaviour
{
    public Slider brightnessSlider, saturationSlider, musicVolumeSlider, sfxVolumeSlider;
    public float brightnessDefaultVal, saturationDefaultVal, musicVolumeDefaultVal, sfxVolumeDefaultVal;

    private void Start()
    {
        if(brightnessSlider) brightnessSlider.value = brightnessDefaultVal;
        if(saturationSlider) saturationSlider.value = saturationDefaultVal;
        if(musicVolumeSlider) musicVolumeSlider.value = musicVolumeDefaultVal;
        if(sfxVolumeSlider) sfxVolumeSlider.value = sfxVolumeDefaultVal;
        
        OnBrightnessValueChanged(brightnessDefaultVal);
        OnSaturationValueChanged(saturationDefaultVal);
        OnMusicVolumeValueChanged(musicVolumeDefaultVal);
        OnSFXVolumeValueChanged(sfxVolumeDefaultVal);
        
        Toggle();
    }

    public void OnBrightnessValueChanged(float val)
    {
        GameEventManager.Instance.OnBrightnessChange(val);
    }

    public void OnSaturationValueChanged(float val)
    {
        GameEventManager.Instance.OnSaturationChange(val);
    }

    public void OnMusicVolumeValueChanged(float val)
    {
        GameEventManager.Instance.OnMusicVolumeChange(val);
    }
    
    public void OnSFXVolumeValueChanged(float val)
    {
        GameEventManager.Instance.OnSoundVolumeChange(val);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
