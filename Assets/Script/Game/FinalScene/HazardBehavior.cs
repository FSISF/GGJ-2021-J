using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehavior : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        var dino = other.gameObject.GetComponent<Dragon>();
        if (!dino) return;
        
        dino.SetState(eDragonState.Injurd);
        
        Destroy(gameObject);
    }
}
