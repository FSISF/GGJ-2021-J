using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResizeAsRect : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    
    // Start is called before the first frame update
    void Start()
    {
        var corners = new Vector3[4];
        target.GetWorldCorners(corners);
        var box= GetComponent<BoxCollider2D>();

        box.size = new Vector2(corners.Max(i => i.x) - corners.Min(i => i.x),
            corners.Max(i => i.y) - corners.Min(i => i.y));
        box.transform.position = new Vector3(corners.Average(i => i.x), corners.Average(i => i.y));
    }
}
