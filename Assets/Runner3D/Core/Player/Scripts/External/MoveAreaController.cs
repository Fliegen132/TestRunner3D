using Assets;
using Core.Canvas;
using Events;
using OrderMaster;
using Services;
using UnityEngine;

namespace Core.Player
{
    [Order(10)]
    public class MoveAreaController : MonoBehaviour, IControlEntity
    {
        private const string m_MoveAreaName = "MoveAreaView";

        private EventManager m_EventManager;
        private AssetLoader m_AssetLoader;
        private MoveAreaView m_MoveArea;

        public void PreInit()
        {
            m_EventManager = ServiceLocator.Current.Get<EventManager>();
            m_AssetLoader = ServiceLocator.Current.Get<AssetLoader>();
        }

        public void Initing()
        {
            var moveArea = m_AssetLoader.Load<GameObject>(m_MoveAreaName);
            var parent = ServiceLocator.Current.Get<CoreCanvasController>().GetCoreCanvas();
            m_MoveArea = m_AssetLoader.InstantiateSync<MoveAreaView>(moveArea, parent);
            m_MoveArea.Init(m_EventManager);
        }

        public void PostInit()
        {

        }


    }
}
