using Assets;
using Core.Canvas;
using Core.Player;
using Events;
using OrderMaster;
using Services;
using UnityEngine;

namespace Core.Wealth
{
    [Order(12)]
    public class WealthController : MonoBehaviour, IControlEntity, IService
    {
        private WealthView m_WealthView;
        private EventManager m_EventManager;
        private AssetLoader m_AssetLoader;
        private const string m_WeathViewAsset = "WealthView";
        private WealthDTO m_Wealth;
        private CoreCanvasController m_CoreCanvasController;
        private Player.Player m_Player;

        public void PreInit()
        {
            ServiceLocator.Current.Register(this);

            m_EventManager = ServiceLocator.Current.Get<EventManager>();
            m_AssetLoader = ServiceLocator.Current.Get<AssetLoader>();
            m_Wealth = new WealthDTO(m_EventManager);

            m_CoreCanvasController = ServiceLocator.Current.Get<CoreCanvasController>();
            m_Player = ServiceLocator.Current.Get<PlayerController>().GetPlayer();

        }

        public void Initing()
        {
            var parent = m_CoreCanvasController.GetCoreCanvas();
            var wealth = m_AssetLoader.Load<GameObject>(m_WeathViewAsset);
            m_WealthView = m_AssetLoader.InstantiateSync<WealthView>(wealth.gameObject, parent);
            m_EventManager.Subscribe<ChangeWealthSignal, int>(this, ChangeWealth);

            m_WealthView.ChangeValueText(m_Wealth.GetWealth());
            m_Player.WealthSlider.ChangeValue(m_Wealth.GetWealth());
        }

        public void PostInit() { }
        
        private void ChangeWealth(int value)
        {
            m_Wealth.ChangeWealth(value);
            m_Player.WealthSlider.ChangeValue(value);
            m_WealthView.ChangeValueText(m_Wealth.GetWealth());
        }

        public int GetWealth() => m_Wealth.GetWealth();

        private void OnDestroy()
        {
            m_EventManager.Unsubscribe<ChangeWealthSignal>(this);
        }
    }
}

