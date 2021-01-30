using System;
using Script.Game;
using UnityEngine;

namespace Script.UI
{
    public class StartButtonBreaker: MonoBehaviour
    {
        [SerializeField, Min(0)] private int breakCount;
        [SerializeField] private string targetLayer = "Player";

        private int count;
        
        private void Start()
        {
            RestCount();
            GetComponent<Collider2D>().isTrigger = false;
        }

        private void RestCount()
        {
            count = breakCount;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (count <= 0) return;
            
            if (other.gameObject.layer == LayerMask.NameToLayer(targetLayer))
                count--;

            if (count <= 0)
            {
                GameEventManager.Instance.OnStartButtonOnHit();
                GetComponent<Collider2D>().isTrigger = true;
#if true && UNITY_EDITOR
                Debug.Log("Start Button Break");
#endif
            }
        }
    }
}