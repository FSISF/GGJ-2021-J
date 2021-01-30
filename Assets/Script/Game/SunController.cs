using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;

public class SunController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnBrightnessChanged(0);
    }

    private void OnBrightnessChanged(float val)
    {
        transform.localScale = val * Vector3.one;
    }

    private void OnEnable()
    {
        GameEventManager.Instance.BrightnessChange += OnBrightnessChanged;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.BrightnessChange -= OnBrightnessChanged;
    }
}
