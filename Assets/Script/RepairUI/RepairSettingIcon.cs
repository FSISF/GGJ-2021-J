using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RepairSettingIcon : MonoBehaviour
{
    [SerializeField] private string targetName;

    private bool active;

    private void Start()
    {
        GameEventManager.Instance.StartButtonOnHit += Rest;
    }

    private void Rest()
    {
        active = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!active || other.transform.name != targetName) return;

        GameEventManager.Instance.OnSettingIconBack();
        Destroy(other.gameObject);
        active = false;
    }
}
 