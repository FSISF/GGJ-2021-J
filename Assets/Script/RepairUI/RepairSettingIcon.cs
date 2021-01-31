using Script.Game;
using UnityEngine;

namespace Script.RepairUI
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class RepairSettingIcon : MonoBehaviour
    {
        [SerializeField] private string targetName;
        [SerializeField] private GameObject settingButton;
        [SerializeField] private GameObject tip;

        private bool _active;

        private void Start()
        {
            GameEventManager.Instance.StartButtonOnHit += Rest;
            settingButton.SetActive(false);
            tip.SetActive(false);
        }

        private void Rest()
        {
            _active = true;
            tip.SetActive(true);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_active || other.transform.name != targetName) return;

            GameEventManager.Instance.OnSettingIconBack();
            settingButton.SetActive(true);
            tip.SetActive(false);
            Destroy(other.gameObject);
            _active = false;
        }
    }
}
 