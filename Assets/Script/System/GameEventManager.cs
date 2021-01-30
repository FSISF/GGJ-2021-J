using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Script.Game
{
    public class GameEventManager : Singleton<GameEventManager>
    {
        #region Event

        #region UI

        public event Action SettingIconBack;

        public event Action HealthPenalBack;

        public event Action ScorePenalBack;

        public event Action HighScorePenalBack;

        public event Action<float> BrightnessChange;
        
        public event Action<float> SaturationChange;

        public event Action<float> SoundVolumeChange;

        public event Action<float> MusicVolumeChange;

        #endregion

        #region SceneRepair

        /// <summary>
        /// 開始按鈕被撞壞
        /// </summary>
        public event Action StartButtonOnHit;

        /// <summary>
        /// 找回太陽
        /// </summary>
        public event Action SunBack;

        /// <summary>
        /// 地板修復
        /// </summary>
        public event Action GroundCompleted;

        /// <summary>
        /// 仙人掌長出
        /// </summary>
        public event Action CactusCompleted;

        /// <summary>
        /// 翼龍尋回
        /// </summary>
        public event Action PterodactylBack;

        /// <summary>
        /// 天空恢復
        /// </summary>
        public event Action SkyComplete;

        /// <summary>
        /// 隕石掉到地上
        /// </summary>
        public event Action MeteoriteFall;

        /// <summary>
        /// 恐龍跟隕石掉到地下
        /// </summary>
        public event Action DinoFall;

        /// <summary>
        /// 找到金幣 
        /// </summary>
        public event Action GoldFound;

        /// <summary>
        /// 恐龍回到地上
        /// </summary>
        public event Action DinoBack;

        #endregion

        #region DinoState

        /// <summary>
        /// 恐龍得到金幣
        /// </summary>
        public event Action DinoCatchGold;

        public event Action<int> DinoHeart;

        /// <summary>
        /// 恐龍死亡
        /// </summary>
        public event Action DinoDead;

        #endregion

        #endregion

        #region RiseEvent

        public void OnStartButtonOnHit()
        {
#if true || UNITY_EDITOR
            Debug.Log("StartButtonOnHit");
#endif
            StartButtonOnHit?.Invoke();
        }

        public void OnSettingIconBack()
        {
#if true || UNITY_EDITOR
            Debug.Log("SettingIconBack");
#endif
            SettingIconBack?.Invoke();
        }
        public void OnSaturationChange(float saturation) => SaturationChange?.Invoke(saturation);

        public void OnHealthPenalBack()
        {
#if true || UNITY_EDITOR
            Debug.Log("HealthPenalBack");
#endif
            HealthPenalBack?.Invoke();
        }

        public void OnScorePenalBack()
        {
#if true || UNITY_EDITOR
            Debug.Log("ScorePenalBack");
#endif
            ScorePenalBack?.Invoke();
        }

        public void OnHighScorePenalBack()
        {
#if true || UNITY_EDITOR
            Debug.Log("HighScorePenalBack");
#endif
            HighScorePenalBack?.Invoke();
        }

        public void OnBrightnessChange(float brightness)
        {
#if true || UNITY_EDITOR
            Debug.Log("BrightnessChange" + " : " + brightness);
#endif
            BrightnessChange?.Invoke(brightness);
        }

        public void OnSoundVolumeChange(float volume)
        {
#if true || UNITY_EDITOR
            Debug.Log("SoundVolumeChange" + " : " +volume);
#endif
            SoundVolumeChange?.Invoke(volume);
        }

        public void OnMusicVolumeChange(float volume)
        {
#if true || UNITY_EDITOR
            Debug.Log("MusicVolumeChange" + " : " +volume);
#endif
            MusicVolumeChange?.Invoke(volume);
        }

        public void OnSunBack()
        {
#if true || UNITY_EDITOR
            Debug.Log("SunBack");
#endif
            SunBack?.Invoke();
        }

        public void OnGroundCompleted()
        {
#if true || UNITY_EDITOR
            Debug.Log("GroundCompleted");
#endif
            GroundCompleted?.Invoke();
        }

        public void OnCactusCompleted()
        {
#if true || UNITY_EDITOR
            Debug.Log("CactusCompleted");
#endif
            CactusCompleted?.Invoke();
        }

        public void OnPterodactylBack()
        {
#if true || UNITY_EDITOR
            Debug.Log("PterodactylBack");
#endif
            PterodactylBack?.Invoke();
        }

        public void OnSkyCompleted()
        {
#if true || UNITY_EDITOR
            Debug.Log("SkyComplete");
#endif
            SkyComplete?.Invoke();
        }

        public void OnGoldFound()
        {
#if true || UNITY_EDITOR
            Debug.Log("GoldFound");
#endif
            GoldFound?.Invoke();
        }

        public void OnMeteoriteFall()
        {
#if true || UNITY_EDITOR
            Debug.Log("MeteoriteFall");
#endif
            MeteoriteFall?.Invoke();
        }

        public void OnDinoFall()
        {
#if true || UNITY_EDITOR
            Debug.Log("DinoFall");
#endif
            DinoFall?.Invoke();
        }

        public void OnDinoBack()
        {
#if true || UNITY_EDITOR
            Debug.Log("DinoBack");
#endif
            DinoBack?.Invoke();
        }

        public void OnDinoCatchGold()
        {
#if true || UNITY_EDITOR
            Debug.Log("DinoCatchGold");
#endif
            DinoCatchGold?.Invoke();
        }

        public void OnDinoHeart(int health)
        {
#if true || UNITY_EDITOR
            Debug.Log("DinoHeart" + " health left : " + health);
#endif
            DinoHeart?.Invoke(health);
        }

        public void OnDinoDead()
        {
#if true || UNITY_EDITOR
            Debug.Log("DinoDead");
#endif
            DinoDead?.Invoke();
        }

        #endregion
    }
}