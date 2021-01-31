using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Dragon>())
        {
            GameEventManager.Instance.OnDinoCatchGold();
            Destroy(gameObject);
        }
    }
}
