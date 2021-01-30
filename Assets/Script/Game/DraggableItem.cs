using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Rigidbody2D rigidbody;

    [SerializeField, Tooltip("Ignore Gravity When Drag")]
    private bool ignoreGravity = true;

    private new Camera camera;
    private float gravity;

    private void Start()
    {
        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var pointPosition = camera.ScreenToWorldPoint(eventData.position);
        pointPosition.z = transform.position.z;
        transform.position = pointPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (rigidbody is null || !ignoreGravity) return;

        gravity = rigidbody.gravityScale;
        rigidbody.gravityScale = 0;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (rigidbody is null || !ignoreGravity) return;

        rigidbody.gravityScale = gravity;
    }
}