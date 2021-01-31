using Script.Game;
using UnityEngine;

namespace Script.RepairUI
{
    public class ActiveWhenGoldFound : MonoBehaviour
    {
        void Start()
        {
        GameEventManager.Instance.GoldFound += OnGoldFound;
        gameObject.SetActive(false);
        }

        private void OnGoldFound()
        {
            gameObject.SetActive(true);
            GameEventManager.Instance.GoldFound -= OnGoldFound;
        }
    }
}
