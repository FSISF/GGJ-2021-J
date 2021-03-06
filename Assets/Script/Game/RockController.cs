using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class RockController : MonoBehaviour
{
    public string targetTriggerName = "RockBox";

    public GameObject floorObject;

    private void Start()
    {
        floorObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != targetTriggerName) return;
        
        GameEventManager.Instance.OnGroundCompleted();
        floorObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
