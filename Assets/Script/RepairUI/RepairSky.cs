using Script.Game;
using UnityEngine;

namespace Script.RepairUI
{
    public class RepairSky : MonoBehaviour
    {
        void Start()
        {
            GameEventManager.Instance.SkyComplete += () => gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
