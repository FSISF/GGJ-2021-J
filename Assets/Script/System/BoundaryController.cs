using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Script.System
{
    public class BoundaryController : SingletonMono<BoundaryController>
    {
        private GameObject _top, _bottom, _left, _right;
        
        private void Start()
        {
            var verticalOffset = Camera.main.orthographicSize;
            var horizontalOffset = verticalOffset * Camera.main.aspect;

            _top = CreateBound("TopBound",
                new Vector3(0, verticalOffset + 0.5f),
                new Vector2(horizontalOffset * 2 + 2, 1));
            _bottom = CreateBound("BottomBound",
                new Vector3(0, -(verticalOffset + 0.5f)),
                new Vector2(horizontalOffset * 2 + 2, 1));
            _right = CreateBound("RightBound",
                new Vector3(horizontalOffset + 0.5f, 0),
                new Vector2(1, verticalOffset * 2 + 2));
            _left = CreateBound("LeftBound",
                new Vector3(-(horizontalOffset + 0.5f), 0),
                new Vector2(1, verticalOffset * 2 + 2));
        }

        public void EnableLeftRightBound(bool enable)
        {
            _left.SetActive(enable);
            _right.SetActive(enable);
        }

        public void EnableTopBound(bool enable)
        {
            _top.SetActive(enable);
        }

        public void EnableBottomBound(bool enable)
        {
            _bottom.SetActive(enable);
        }
        
        private GameObject CreateBound(string objectName, Vector3 transformPosition, Vector2 boundSize)
        {
            var result = new GameObject(objectName, typeof(BoxCollider2D));
            result.transform.SetParent(transform);
            result.transform.position = transformPosition;
            var bound = result.GetComponent<BoxCollider2D>();
            bound.size = boundSize;
            return result;
        }
    }
}