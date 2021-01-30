using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBGM
{
    None,
    BGM,
}

[System.Serializable]
public class BGMData
{
    public eBGM BGM;
    public AudioClip AudioClip;
}

public enum eSound
{
    None,
    Coin,
    Explosion,
    Jump,
    Death,
    HighScore,
    Hit,
}

[System.Serializable]
public class SoundData
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

        Script.Game.GameEventManager.Instance.MusicVolumeChange += SetVolumeBGM;
        Script.Game.GameEventManager.Instance.SoundVolumeChange += SetVolumeSound;

        SetVolumeBGM(0f);
        SetVolumeSound(0f);

        PlayBGM(eBGM.BGM);
    }

    public void PlayBGM(eBGM bgm, bool loop = true)
    {
        BGMData bgmData = BGM.Find(data => data.BGM == bgm);
        if (bgmData != null)
        {
            AudioSource_BGM.clip = bgmData.AudioClip;
            AudioSource_BGM.loop = loop;
            AudioSource_BGM.Play();
        }
    }

    public void PlaySound(eSound sound)
    {
        SoundData soundData = Sound.Find(data => data.Sound == sound);
        if (soundData != null)
        {
            AudioSource_Sound.PlayOneShot(soundData.AudioClip);
        }
    }

    public void MuteBGM(bool isMute)
    {
        AudioSource_BGM.mute = isMute;
    }

    public void MuteSound(bool isMute)
    {
        AudioSource_Sound.mute = isMute;
    }

    public void SetVolumeBGM(float volume)
    {
        AudioSource_BGM.volume = volume;
    }

    public void SetVolumeSound(float volume)
    {
        AudioSource_Sound.volume = volume;
    }
}