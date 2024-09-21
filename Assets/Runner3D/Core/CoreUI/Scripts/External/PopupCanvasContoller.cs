using Assets;
using Events;
using OrderMaster;
using Services;
using UnityEngine;

namespace Core.Canvas
{
    [Order(-998)]
    public class PopupCanvasContoller : MonoBehaviour, IControlEntity, IService
    {
        private const string m_CanvasName = "PopupCanvas";

        private EventManager m_EventManager;
        private AssetLoader m_AssetLoader;
        private PopupCanvas m_PopupCanvas;

        public void PreInit()
        {
            ServiceLocator.Current.Register(this);
            m_EventManager = ServiceLocator.Current.Get<EventManager>();
            m_AssetLoader = ServiceLocator.Current.Get<AssetLoader>();
        }

        public void Initing()
        {
            var popupCanvas = m_AssetLoader.Load<GameObject>(m_CanvasName);
            m_PopupCanvas = m_AssetLoader.InstantiateSync<PopupCanvas>(popupCanvas, null);

            m_EventManager.Subscribe<OpenLosePopupSignal>(this, OpenLoseWindow);
            m_EventManager.Subscribe<OpenWinPopupSignal>(this, OpenWinWindow);
        }
      
        public void PostInit()
        {
        }

        private void OpenLoseWindow()
        {
            m_PopupCanvas.ActiveLoseWindow();
        }

        private void OpenWinWindow()
        {
            m_PopupCanvas.ActiveWinWindow();
        }

        public Transform GetPopupTransform() => m_PopupCanvas.transform;
    }
}