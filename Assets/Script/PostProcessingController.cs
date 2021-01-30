using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    public PostProcessVolume volume;

    public Vector2 brightnessControlRange = new Vector2(-10, 0);

    private ColorGrading gradingSettings;
    
    // Start is called before the first frame update
    void Start()
    {
        if(volume) volume.profile.TryGetSettings(out gradingSettings);
        if (gradingSettings)
        {
            OnBrightnessChanged(0);
        }
    }

    void OnBrightnessChanged(float val)
    {
        if (!gradingSettings) return;

        gradingSettings.postExposure.Override(Mathf.Lerp(brightnessControlRange.x, brightnessControlRange.y, val));
    }

    private void OnEnable()
    {
        GameEventManager.Instance.BrightnessChange += OnBrightnessChanged;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.BrightnessChange -= OnBrightnessChanged;
    }

    private void Reset()
    {
        volume = GetComponent<PostProcessVolume>();
    }
}
