using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;

[RequireComponent(typeof(CameraController))]
public class CameraBoundSetter : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;

    [SerializeField] private float leftRightBound;
    [SerializeField] private float topBound;
    [SerializeField] private float bottomBound;

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
        GameEventManager.Instance.CactusCompleted += () =>
            cameraController.boundaryRightTop = new Vector2(cameraController.boundaryRightTop.x, topBound);
        GameEventManager.Instance.MeteoriteFall += () =>
            cameraController.boundaryRightTop = new Vector2(cameraController.boundaryRightTop.x, 0);
        GameEventManager.Instance.DinoFall += () =>
            cameraController.boundaryLeftBottom = new Vector2(cameraController.boundaryLeftBottom.x, -bottomBound);
        GameEventManager.Instance.DinoBack += () =>
            cameraController.boundaryLeftBottom = cameraController.boundaryRightTop = Vector2.zero;

        void OnBrightnessChange(float value)
        {
            if (value < 0.9f) return;

            cameraController.boundaryLeftBottom = new Vector2(-leftRightBound, 0);
            cameraController.boundaryRightTop = new Vector2(leftRightBound, 0);

            GameEventManager.Instance.BrightnessChange -= OnBrightnessChange;
        }
    }
}