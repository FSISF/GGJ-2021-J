using Script.Game;
using UnityEngine;

namespace Script.TestHelper
{
    public class EventTrigger : MonoBehaviour
    {
        [ContextMenu("RiseBrightnessUp")]
        public void RiseBrightnessUp()
        {
            GameEventManager.Instance.OnBrightnessChange(1);
        }
        
        [ContextMenu("RiseBrightnessDown")]
        public void RiseBrightnessDown()
        {
            GameEventManager.Instance.OnBrightnessChange(0);
        }
    }
}
