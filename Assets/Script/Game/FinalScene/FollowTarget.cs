using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 15f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            var currPos = transform.position;
            currPos.x = Mathf.Lerp(currPos.x, target.position.x, Time.fixedDeltaTime * smoothSpeed);
            transform.position = currPos;
        }
    }
}
