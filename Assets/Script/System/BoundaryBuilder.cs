using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Script.System
{
    public class BoundaryBuilder : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D floor;
        [SerializeField] private string layerName;

        private void Start()
        {
            var verticalOffset = Camera.main.orthographicSize;
            var horizontalOffset = verticalOffset * Camera.main.aspect;

            CreateBound("TopBound",
                new Vector3(0, verticalOffset + 0.5f),
                new Vector2(horizontalOffset * 2 + 2, 1));
            CreateBound("BottomBound",
                new Vector3(0, -(verticalOffset + 0.5f)),
                new Vector2(horizontalOffset * 2 + 2, 1));
            CreateBound("RightBound",
                new Vector3(horizontalOffset + 0.5f, 0),
                new Vector2(1, verticalOffset * 2 + 2));
            CreateBound("LeftBound",
                new Vector3(-(horizontalOffset + 0.5f), 0),
                new Vector2(1, verticalOffset * 2 + 2));

            floor.size = new Vector2(horizontalOffset * 2, floor.size.y);
        }

        private void CreateBound(string objectName, Vector3 transformPosition, Vector2 boundSize)
        {
            var result = new GameObject(objectName, typeof(BoxCollider2D));
            result.transform.SetParent(transform);
            result.transform.position = transformPosition;
            layerName = "Player";
            result.layer = LayerMask.NameToLayer(layerName);
            var bound = result.GetComponent<BoxCollider2D>();
            bound.size = boundSize;
        }
    }
}