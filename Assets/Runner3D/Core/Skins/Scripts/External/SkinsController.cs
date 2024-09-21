using Core.Player;
using Core.Skin;
using Core.Wealth;
using Events;
using OrderMaster;
using Services;
using UnityEngine;

namespace Core.Skins
{
    [Order(14)]
    public class SkinsController : MonoBehaviour, IControlEntity, IService
    {
        private EventManager m_EventManager;
        private WealthController m_WealthController;
        private PlayerController m_PlayerController;
        private SkinsView m_SkinsView;
        private GameObject m_CurrentSkinObject;
        private Skin.Skin m_CurrentSkin;

        public void PreInit()
        {
            ServiceLocator.Current.Register(this);
            m_EventManager = ServiceLocator.Current.Get<EventManager>();
            m_WealthController = ServiceLocator.Current.Get<WealthController>();
            m_PlayerController = ServiceLocator.Current.Get<PlayerController>();
            m_SkinsView = m_PlayerController.GetPlayer().WealthSkins;
        }

        public void Initing()
        {
            ChangeSkin();
            m_EventManager.Subscribe<ChangeSkinSignal>(this, ChangeSkin);
        }

        public void PostInit()
        {

        }

        private void ChangeSkin()
        {
            m_CurrentSkinObject = FindSkin();
            m_CurrentSkinObject.SetActive(true);
        }

        private GameObject FindSkin()
        {
            GameObject bestSkin = null;

            foreach (var skin in m_SkinsView.Skins)
            {
                if (skin.Value <= m_WealthController.GetWealth()) 
                {
                    m_CurrentSkin = skin;
                    bestSkin = skin.GameObject;
                    m_PlayerController.GetPlayer().WealthSlider.ChangeText(skin.Text);
                }
            }

            if (m_CurrentSkinObject != null && bestSkin != null)
            {
                m_CurrentSkinObject.SetActive(false);
            }

            return bestSkin; 
        }

        public Skin.Skin GetCurrentSkin() => m_CurrentSkin;
    }
}
