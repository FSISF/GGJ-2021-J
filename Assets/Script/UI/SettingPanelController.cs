using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;

public class SettingPanelController : MonoBehaviour
{
    public void OnBrightnessValueChanged(float val)
    {
        GameEventManager.Instance.OnBrightnessChange(val);
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
