using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGround : MonoBehaviour {
    #region Variables

    public Transform center;
    public float groundSize = 43.0f;
    public List<Transform> grounds = new List<Transform>();

    #endregion

    #region Private Function

    private void Resort() {
        if (center == null)
            return;

        var halfSize = groundSize / 2;
        var first = grounds[0].transform;
        var last = grounds[grounds.Count - 1].transform;
        var centerX = center.transform.position.x;

        var d1 = Mathf.Abs(first.position.x - centerX);
        var d2 = Mathf.Abs(last.position.x - centerX);

        if (Mathf.Abs(d1 - d2) < groundSize)
            return;

        if (d1 > d2) {
            grounds.RemoveAt(0);

            var newPosition = last.position;

            newPosition.x += groundSize;

            first.position = newPosition;

            grounds.Add(first);
        } else {
            grounds.RemoveAt(grounds.Count - 1);

            var newPosition = first.position;

            newPosition.x -= groundSize;

            last.position = newPosition;

            grounds.Insert(0, last);
        }
    }

    #endregion

    #region Behaviour

    private void Start() {
        if (center == null)
            return;

        var centerX = center.position.x;

        foreach (var ground in grounds) {
            var position = ground.transform.position;

            position.x = centerX;

            ground.transform.position = position;

            centerX += groundSize;
        }
    }

    private void Update() {
        Resort();
    }

    #endregion
}
