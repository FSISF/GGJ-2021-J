using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBGM
{
    None,
}

[System.Serializable]
public struct BGMData
{
    public eBGM BGM;
    public AudioClip AudioClip;
}

public enum eSound
{
    None,
    Hit,
    Hit2,
    Hit3,
    Jump,
    Jump2,
    Jump3,
}

[System.Serializable]
public struct SoundData
{
    public eSound Sound;
    public AudioClip AudioClip;
}

public class MusicSystem : SingletonMono<MusicSystem>
{
    public List<BGMData> BGM = new List<BGMData>();
    public List<SoundData> Sound = new List<SoundData>();

    public AudioSource AudioSource_BGM;
    public AudioSource AudioSource_Sound;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}