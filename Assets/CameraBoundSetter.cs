using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;

[RequireComponent(typeof(CameraController))]
public class CameraBoundSetter : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    
    private void Start()
    {
        if (!cameraController)
            cameraController = GetComponent<CameraController>();

        cameraController.boundaryLeftBottom = cameraController.boundaryRightTop = Vector2.zero;

        GameEventManager.Instance.BrightnessChange += OnBrightnessChange;
        GameEventManager.Instance.ScorePenalBack += () =>
        {
            cameraController.boundaryLeftBottom = new Vector2(0, cameraController.boundaryLeftBottom.y);
            cameraController.boundaryRightTop = new Vector2(0, cameraController.boundaryRightTop.y);
        };
        
        void OnBrightnessChange(float value)
        {
            if (value < 0.9f) return;

            cameraController.boundaryLeftBottom = new Vector2(-20, 0);
            cameraController.boundaryRightTop = new Vector2(20, 0);

            GameEventManager.Instance.BrightnessChange -= OnBrightnessChange;
        }
    }
}
