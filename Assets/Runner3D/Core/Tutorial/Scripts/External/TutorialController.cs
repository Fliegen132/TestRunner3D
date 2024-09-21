using Assets;
using Core.Canvas;
using Events;
using OrderMaster;
using Services;
using UnityEngine;

namespace Core.Tutorial
{
    [Order(15)]
    public class TutorialController : MonoBehaviour, IControlEntity
    {
        private const string m_TutorialName = "TutorialView";

        private EventManager m_EventManager;
        private AssetLoader m_AssetLoader;
        private TutorialView m_TutorialView;
        private CoreCanvasController m_CoreCanvasController;

        public void PreInit()
        {
            m_EventManager = ServiceLocator.Current.Get<EventManager>();
            m_AssetLoader = ServiceLocator.Current.Get<AssetLoader>();
            m_CoreCanvasController = ServiceLocator.Current.Get<CoreCanvasController>();
        }

        public void Initing()
        {
            var parent = m_CoreCanvasController.GetCoreCanvas();
            var tutor = m_AssetLoader.Load<GameObject>(m_TutorialName);

            m_TutorialView = m_AssetLoader.InstantiateSync<TutorialView>(tutor, parent.transform);
            m_EventManager.Subscribe<CloseTutorialSignal>(this, CloseWindow);
            m_EventManager.Subscribe<OpenTutorialSignal>(this, OpenWindow);
        }

        public void PostInit() { }
        
        private void CloseWindow()
        {
            if (!m_TutorialView.GetActive())
            {
                return;
            }

            m_TutorialView.SetActive(false);
        }

        private void OpenWindow()
        {
            if (m_TutorialView.GetActive())
            {
                return;
            }

            m_TutorialView.SetActive(true);
        }

        private void OnDestroy()
        {
            m_EventManager.Unsubscribe<CloseTutorialSignal>(this);
            m_EventManager.Unsubscribe<OpenTutorialSignal>(this);
        }
    }
}

