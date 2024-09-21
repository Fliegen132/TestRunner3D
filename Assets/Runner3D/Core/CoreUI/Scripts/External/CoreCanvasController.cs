using Assets;
using Events;
using OrderMaster;
using Services;
using UnityEngine;

namespace Core.Canvas
{
    [Order(-999)]
    public class CoreCanvasController : MonoBehaviour, IControlEntity, IService
    {
        private const string m_CanvasName = "CoreCanvas";

        private EventManager m_EventManager;
        private AssetLoader m_AssetLoader;
        private CoreCanvas m_CoreCanvas;

        public void PreInit()
        {
            ServiceLocator.Current.Register(this);
            m_EventManager = ServiceLocator.Current.Get<EventManager>();
            m_AssetLoader = ServiceLocator.Current.Get<AssetLoader>();
        }

        public void Initing()
        {
            var coreCanvas = m_AssetLoader.Load<GameObject>(m_CanvasName);
            m_CoreCanvas = m_AssetLoader.InstantiateSync<CoreCanvas>(coreCanvas, null);
            m_EventManager.Subscribe<CloseCoreCanvasSignal>(this, CloseWindow);
        }

        public void PostInit()
        {

        }

        private void CloseWindow()
        {
            m_CoreCanvas.SetActive(false);
        }


        public Transform GetCoreCanvas()
        {
            return m_CoreCanvas.GetTransform();
        }
    }
}

