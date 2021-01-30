using System;
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

        public void OnStartButtonOnHit() => StartButtonOnHit?.Invoke();
        public void OnSettingIconBack() => SettingIconBack?.Invoke();
        public void OnHealthPenalBack() => HealthPenalBack?.Invoke();
        public void OnScorePenalBack() => ScorePenalBack?.Invoke();
        public void OnHighScorePenalBack() => HighScorePenalBack?.Invoke();

        public void OnBrightnessChange(float brightness) => BrightnessChange?.Invoke(brightness);
        public void OnSoundVolumeChange(float volume) => SoundVolumeChange?.Invoke(volume);
        public void OnMusicVolumeChange(float volume) => MusicVolumeChange?.Invoke(volume);

        public void OnSunBack() => SunBack?.Invoke();
        public void OnGroundCompleted() => GroundCompleted?.Invoke();
        public void OnCactusCompleted() => CactusCompleted?.Invoke();
        public void OnPterodactylBack() => PterodactylBack?.Invoke();
        public void OnSkyCompleted() => SkyComplete?.Invoke();
        public void OnGoldFound() => GoldFound?.Invoke();
        public void OnMeteoriteFall() => MeteoriteFall?.Invoke();
        public void OnDinoFall() => DinoFall?.Invoke();
        public void OnDinoBack() => DinoBack?.Invoke();

        public void OnDinoCatchGold() => DinoCatchGold?.Invoke();
        public void OnDinoHeart(int health) => DinoHeart?.Invoke(health);
        public void OnDinoDead() => DinoDead?.Invoke();

        #endregion
    }
}