using System;
using Script.Game;
using UnityEngine;

namespace Script.UI
{
    public class StartButtonBreaker : MonoBehaviour
    {
        [SerializeField, Min(0)] private int breakCount;
        [SerializeField] private string targetLayer = "Player";

        [SerializeField] private GameObject settingIcon;

        private int count;
        private GameObject settingIconInstance;

        private void Start()
        {
            Rest();
            GameEventManager.Instance.StartButtonOnHit += SpawnSettingIcon;
            GetComponent<Collider2D>().isTrigger = false;
        }

        private void Rest()
        {
            count = breakCount;
            if (settingIconInstance) Destroy(settingIconInstance);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (count <= 0) return;

            if (other.gameObject.layer == LayerMask.NameToLayer(targetLayer))
                count--;

            if (count > 0) return;

            GetComponent<Collider2D>().isTrigger = true;
#if true && UNITY_EDITOR
            Debug.Log("Start Button Break");
#endif
            GameEventManager.Instance.OnStartButtonOnHit();
        }

        private void SpawnSettingIcon()
        {
            settingIconInstance = Instantiate(settingIcon, transform.position, Quaternion.identity);
            settingIconInstance.name = settingIcon.name;
        }
    }
}