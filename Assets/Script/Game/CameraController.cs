using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool lockMouse = true;
    
    public Vector2 boundaryLeftBottom;

    public Vector2 boundaryRightTop;

    public float boundaryDetectionRange = 200;

    public float moveSpeed = 1f;

    public float smoothSpeed = 5f;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
        if(lockMouse) Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Vector3.zero;

        if (Input.mousePosition.x <= boundaryDetectionRange)
        {
            movement.x = -moveSpeed;
        }
        else if (Input.mousePosition.x >= Screen.width - boundaryDetectionRange)
        {
            movement.x = moveSpeed;
        }
        
        if (Input.mousePosition.y <= boundaryDetectionRange)
        {
            movement.y = -moveSpeed;
        }
        else if (Input.mousePosition.y >= Screen.height - boundaryDetectionRange)
        {
            movement.y = moveSpeed;
        }

        targetPosition.x = Mathf.Clamp(targetPosition.x + movement.x * Time.deltaTime, 
            boundaryLeftBottom.x,
            boundaryRightTop.x);
        
        targetPosition.y = Mathf.Clamp(targetPosition.y + movement.y * Time.deltaTime, 
            boundaryLeftBottom.y,
            boundaryRightTop.y);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
    }
}
