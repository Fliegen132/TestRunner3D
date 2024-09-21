using Core.Door;
using Core.Point;
using Core.Skins;
using Events;
using Services;
using UnityEngine;

namespace Core.Player
{
    public class TriggerEvent : MonoBehaviour
    {
        private EventManager m_EventManager;
        private SkinsController m_SkinsController;

        public void Init(EventManager eventManager)
        {
            m_EventManager = eventManager;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out IPickable pickuble))
            { 
                int value = pickuble.Pickup();
                m_EventManager.TriggerEvenet<ChangeWealthSignal, int>(value);
            }

            if (collider.TryGetComponent(out CheckPoint checkPoint))
            {
                checkPoint.Trigger();
            }

            if (collider.TryGetComponent(out EndDoor endDoor))
            {
                if (m_SkinsController == null)
                { 
                    m_SkinsController = ServiceLocator.Current.Get<SkinsController>();
                }
                if (!endDoor.CanOpen(m_SkinsController.GetCurrentSkin().Level))
                {
                    m_EventManager.TriggerEvenet<StopPlayerMoveSignal>();
                    m_EventManager.TriggerEvenet<OpenWinPopupSignal>();
                }
            }
        }
    }
}
