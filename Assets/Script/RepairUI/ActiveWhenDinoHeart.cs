using Script.Game;
using UnityEngine;

namespace Script.RepairUI
{
    public class ActiveWhenDinoHeart : MonoBehaviour
    {
        private void Start()
        {
            GameEventManager.Instance.DinoHeart += OnBrightnessChange;
            gameObject.SetActive(false);

            void OnBrightnessChange(int _)
            {
                gameObject.SetActive(true);
                GameEventManager.Instance.DinoHeart -= OnBrightnessChange;
            }
        }
    }
}