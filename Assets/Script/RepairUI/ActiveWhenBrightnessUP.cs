using Script.Game;
using UnityEngine;

namespace Script.RepairUI
{
    public class ActiveWhenBrightnessUp : MonoBehaviour
    {
        void Start()
        {
            GameEventManager.Instance.BrightnessChange += OnBrightnessChange;
            gameObject.SetActive(false);

            void OnBrightnessChange(float value)
            {
                if (value < 0.9f) return;

                gameObject.SetActive(true);
                GameEventManager.Instance.BrightnessChange -= OnBrightnessChange;
            }
        }
    }
}