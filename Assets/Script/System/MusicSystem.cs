using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : SingletonMono<MusicSystem>
{
    public AudioSource AudioSource_BGM;
    public AudioSource AudioSource_Sound;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}