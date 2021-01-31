using Script.Game;
using UnityEngine;

namespace Script.RepairUI
{
    public class RepairScorePanel : MonoBehaviour
    {
        [SerializeField] private string targetName;
        [SerializeField] private GameObject scorePanel;
        [SerializeField] private GameObject tip;

        private bool _active;
        
        void Start()
        {
            GameEventManager.Instance.GoldFound += () => _active = true;
            GameEventManager.Instance.DinoCatchGold += OnCatchGold;
            scorePanel.SetActive(false);
            scorePanel.SetActive(true);
        }

        private void OnCatchGold()
        {
            if (!_active) return;

            tip.SetActive(true);
            GameEventManager.Instance.DinoCatchGold -= OnCatchGold;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_active || other.gameObject.name != targetName) return;
            
            GameEventManager.Instance.OnScorePenalBack();
            scorePanel.SetActive(true);
            tip.SetActive(false);
            Destroy(other.gameObject);
            _active = false;
        }
    }
}
