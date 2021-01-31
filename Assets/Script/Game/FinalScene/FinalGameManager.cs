using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FinalGameManager : MonoBehaviour
{
    public Dragon dino;

    public List<GameObject> cactusPrefabs;

    public GameObject badDragonPrefab;

    public GameObject coinPrefab;

    public float spawnBaseline = -4.5f;

    public float spawnOffsetX = 12f;

    public float maxSpawnHeight = 5f;

    public Vector2 spawnDistanceRange = new Vector2(8, 16);

    private float dinoFurthest, spawnThreshold;


    private void Start()
    {
        dinoFurthest = dino.transform.position.x;
        spawnThreshold = dinoFurthest + spawnDistanceRange.y;
    }

    // Update is called once per frame
    void Update()
    {
        dinoFurthest = Mathf.Max(dinoFurthest, dino.transform.position.x);
        if (dinoFurthest >= spawnThreshold)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        float spawnType = Random.value;
        if (spawnType < 0.5)
        {
            // Cactus
            var target = Common.GetRandomElement(cactusPrefabs);
            Instantiate(target, new Vector3(spawnThreshold + spawnOffsetX, spawnBaseline, 0), Quaternion.identity);
        }else if (spawnType < 0.7)
        {
            // Bad Dragon
            Instantiate(badDragonPrefab, new Vector3(spawnThreshold + spawnOffsetX, spawnBaseline + Random.value * maxSpawnHeight, 0), Quaternion.identity);
        }
        else
        {
            // Coin
            Instantiate(coinPrefab, new Vector3(spawnThreshold + spawnOffsetX, spawnBaseline + Random.value * maxSpawnHeight, 0), Quaternion.identity);
        }
        spawnThreshold += Random.Range(spawnDistanceRange.x, spawnDistanceRange.y);
    }
}
