using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableItem : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private new Rigidbody2D rigidbody;

    [SerializeField, Tooltip("Ignore Gravity When Drag")]
    private bool ignoreGravity = true;

    private new Camera camera;
    private float gravity;
    private Vector3 offset;

    private void Start()
    {
        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var pointPosition = camera.ScreenToWorldPoint(eventData.position);
        pointPosition.z = transform.position.z;
        //transform.position = pointPosition + offset;
        rigidbody.MovePosition(pointPosition + offset);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (rigidbody is null || !ignoreGravity) return;

        var pointPosition = camera.ScreenToWorldPoint(eventData.position);
        offset = transform.position - pointPosition;
        offset.z = 0;
        gravity = rigidbody.gravityScale;
        rigidbody.gravityScale = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (rigidbody is null || !ignoreGravity) return;

        rigidbody.gravityScale = gravity;
    }
}