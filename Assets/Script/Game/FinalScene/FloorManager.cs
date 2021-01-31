using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public float groundPrefabSize = 43.25f;

    public Transform dino;

    public List<GameObject> groundInstances;
    // Update is called once per frame
    void Update()
    {
        if (dino.position.x > groundInstances[0].transform.position.x + groundPrefabSize * 2.5f)
        {
            var left = groundInstances[0];
            var leftPos = left.transform.position;
            leftPos.x = groundInstances[2].transform.position.x + groundPrefabSize;
            left.transform.position = leftPos;
            groundInstances.RemoveAt(0);
            groundInstances.Add(left);
        }else if (dino.position.x < groundInstances[0].transform.position.x + groundPrefabSize * 0.5f)
        {
            var right = groundInstances[2];
            var rightPos = right.transform.position;
            rightPos.x = groundInstances[0].transform.position.x - groundPrefabSize;
            right.transform.position = rightPos;
            groundInstances.RemoveAt(2);
            groundInstances.Insert(0, right);
        }
    }
}
