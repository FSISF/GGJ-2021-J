using System;
using Script.Game;
using UnityEngine;

namespace Script.RepairUI
{
    [RequireComponent(typeof(Collider2D))]
    public class RepairHealthPanel : MonoBehaviour
    {
        [SerializeField] private string targetName;
        [SerializeField] private GameObject healthPanel;
        [SerializeField] private GameObject tip;

        private bool _active;

        private void Start()
        {
            GameEventManager.Instance.GroundCompleted += () => _active = true;
            GameEventManager.Instance.DinoHeart += OnDinoHeart;
            healthPanel.SetActive(false);
            tip.SetActive(false);
        }

        private void OnDinoHeart(int _)
        {
            if (!_active) return;
            
            tip.SetActive(true);
            GameEventManager.Instance.DinoHeart -= OnDinoHeart;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_active || other.transform.name != targetName) return;
            
            GameEventManager.Instance.OnHealthPenalBack();
            healthPanel.SetActive(true);
            tip.SetActive(false);
            Destroy(other.gameObject);
            _active = false;
        }
    }
}