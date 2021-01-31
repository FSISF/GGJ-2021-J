using Script.Game;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.RepairUI
{
    public class FindCoin : MonoBehaviour, IPointerClickHandler
    {
        private void Start()
        {
            GameEventManager.Instance.DinoFall += OnDinoFall;
            gameObject.SetActive(false);
        }

        private void OnDinoFall()
        {
            gameObject.SetActive(true);
            GameEventManager.Instance.DinoFall -= OnDinoFall;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameEventManager.Instance.OnGoldFound();
            Destroy(gameObject);
        }
    }
}
