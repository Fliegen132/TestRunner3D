using Assets;
using Core.Player;
using Events;
using OrderMaster;
using UnityEngine;

namespace Services
{
    [Order(-9999)]
    public class CORE_SERVICELOCATOR : MonoBehaviour, IControlEntity
    {
        [SerializeField] private PlayerController m_PlayerController;
        private AssetLoader m_AssetLoader;
        private EventManager m_EventManager;
        public void PreInit()
        {
            ServiceLocator locator = new ServiceLocator();

            m_AssetLoader = new AssetLoader();
            m_EventManager = new EventManager();


            ServiceLocator.Current.Register(m_EventManager);
            ServiceLocator.Current.Register(m_AssetLoader);
            ServiceLocator.Current.Register(m_PlayerController);
        }

        public void Initing()
        {
        }

        public void PostInit()
        {
        }

       
    }
}
