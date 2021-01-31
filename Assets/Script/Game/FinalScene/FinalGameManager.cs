using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class FinalGameManager : MonoBehaviour
{
    public Dragon dino;

    public GameObject spaceSprite;

    public List<GameObject> cactusPrefabs;

    public GameObject badDragonPrefab;

    public GameObject coinPrefab;

    public GameObject meteorPrefab;

    public float spawnBaseline = -4.5f;

    public float spawnOffsetX = 12f;

    public float maxSpawnHeight = 5f;

    public Vector2 spawnDistanceRange = new Vector2(8, 16);

    public float spaceThreshold = 1000f;

    public Vector2 meteorDropRelativeRange = new Vector2(-5, 30);

    public float meteorHeight = 10f;

    public AnimationCurve dinoSpeedCurve, meteorDropInterval, spaceSpriteHeight;

    private float dinoFurthest, spawnThreshold;

    private AudioSource bgmSource;

    private bool isSpace = false;
    


    private void Start()
    {
        dinoFurthest = dino.transform.position.x;
        spawnThreshold = dinoFurthest + spawnDistanceRange.y;
        bgmSource = MusicSystem.Instance.AudioSource_BGM;
    }

    private void OnDinoDead()
    {
        bgmSource.volume = 0;
        MusicSystem.Instance.AudioSource_Sound.volume = 0;
        SceneManager.LoadScene(2);
    }

    private void OnEnable()
    {
        GameEventManager.Instance.DinoDead += OnDinoDead;
    }

    // Update is called once per frame
    void Update()
    {
        dinoFurthest = Mathf.Max(dinoFurthest, dino.transform.position.x);
        if (dinoFurthest >= spawnThreshold)
        {
            Spawn();
        }

        dino.moveSpeed = dinoSpeedCurve.Evaluate(dinoFurthest);
        
        var spacePos = spaceSprite.transform.position;
        spacePos.y = spaceSpriteHeight.Evaluate(dinoFurthest);
        spaceSprite.transform.position = spacePos;

        if (!isSpace && dinoFurthest >= spaceThreshold)
        {
            bgmSource.pitch = 1.25f;
            Common.Timer(GetNextMeteorDelay(), DropMeteor);
            isSpace = true;
        }
    }

    private void DropMeteor()
    {
        var pos = new Vector3(
            dino.transform.position.x + Random.Range(meteorDropRelativeRange.x, meteorDropRelativeRange.y),
            meteorHeight, 0);

        Instantiate(meteorPrefab, pos, Quaternion.identity);
        
        Common.Timer(GetNextMeteorDelay(), DropMeteor);
    }

    private float GetNextMeteorDelay()
    {
        return meteorDropInterval.Evaluate(dinoFurthest) * (0.5f + Random.value);
    }

    private void OnDisable()
    {
        if(bgmSource) bgmSource.pitch = 1f;
        GameEventManager.Instance.DinoDead -= OnDinoDead;
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
