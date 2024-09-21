using Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Player
{
    public class MoveAreaView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool m_isDragging;
        private Vector3 m_DraggingStartPos;
        private EventManager m_EventManager;
        private bool m_IsMouseMoving = false;

        public void Init(EventManager eventManager)
        {
            m_EventManager = eventManager;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_isDragging = true;
            m_DraggingStartPos = eventData.position;
            CommonMethods();
        }

        //TODO other class
        private void CommonMethods()
        {
            m_EventManager.TriggerEvenet<CloseTutorialSignal>();
            m_EventManager.TriggerEvenet<StartPlayerMoveSignal>();
        }
        //-------------

        public void Update()
        {
            if (m_isDragging)
            {
                float deltaX = Input.mousePosition.x - m_DraggingStartPos.x;
                m_IsMouseMoving = Input.mousePosition != m_DraggingStartPos;

                if (m_IsMouseMoving)
                {
                    Vector3 direction = new Vector3(deltaX * Time.deltaTime, 0, 0);
                    m_EventManager.TriggerEvenet<SlidePlayerSignal, Vector3>(direction);
                    m_DraggingStartPos = Input.mousePosition;
                }
                else
                {
                    m_EventManager.TriggerEvenet<StopSlidePlayerSignal>();
                }
            }
        }
       
        public void OnPointerExit(PointerEventData eventData)
        {
            m_EventManager.TriggerEvenet<StopSlidePlayerSignal>();
            m_isDragging = false;
        }
    }
}
