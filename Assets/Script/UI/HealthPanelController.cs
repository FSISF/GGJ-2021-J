using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;
using UnityEngine.UI;

public class HealthPanelController : MonoBehaviour
{
    public List<Image> hearts;

    private void OnEnable()
    {
        GameEventManager.Instance.DinoHeart += OnHealthChanged;
    }

    private void OnHealthChanged(int newVal)
    {
        for (var i = 0; i < hearts.Count; i++)
        {
            hearts[i].enabled = i < newVal;
        }
    }

    private void OnDisable()
    {
        GameEventManager.Instance.DinoHeart -= OnHealthChanged;
    }
}
