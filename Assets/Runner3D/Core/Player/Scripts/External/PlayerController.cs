using OrderMaster;
using UnityEngine;
using Events;
using Services;
using Assets;
using Core.Skins;

namespace Core.Player
{
    [Order(11)]
    public class PlayerController : MonoBehaviour, IControlEntity, IService
    {
        private Player m_Player;
        private EventManager m_EventManager;
        private AssetLoader m_AssetLoader;
        private const string m_PlayerAsset = "Player";
        private GameObject m_CurrentSkin;
        public void PreInit()
        {
            m_EventManager = ServiceLocator.Current.Get<EventManager>();
            m_AssetLoader = ServiceLocator.Current.Get<AssetLoader>();
        }

        public void Initing()
        {
            var player = m_AssetLoader.Load<GameObject>(m_PlayerAsset);
            m_Player = m_AssetLoader.InstantiateSync<Player>(player.gameObject, null);
            m_Player.Sides.Init(m_EventManager);
            m_Player.PickupCollector.Init(m_EventManager);
        }
       
        public void PostInit()
        {
            m_EventManager.Subscribe<SlidePlayerSignal, Vector3>(this, Move);
            m_EventManager.Subscribe<StopSlidePlayerSignal>(this, StopSlide);
            m_EventManager.Subscribe<StartPlayerMoveSignal>(this, StartMove);
            m_EventManager.Subscribe<StopPlayerMoveSignal>(this, StopMove);
            m_EventManager.Subscribe<RotatePlayerSignal, float>(this, Rotate);
        }

        private void Move(Vector3 direction)
        {
            m_Player.Slide(direction);
            if (!m_Player.IsSlide)
            { 
                m_Player.IsSlide = true;
            }
        }

        private void StopSlide()
        {
            m_Player.StopSlide();
            m_Player.IsSlide = false;
        }

        private void StartMove()
        {
            if (m_Player.IsMoving)
            {
                return;
            }

            m_Player.IsMoving = true;
        }

        private void StopMove()
        {
            if(!m_Player.IsMoving)
            {
                return;
            }

            m_Player.IsMoving = false;
        }

        private void Rotate(float angle)
        {
            m_Player.SetRotateOrigin(angle);
        }

        
        public Player GetPlayer() => m_Player;
        private void OnDestroy()
        {
            m_EventManager.Unsubscribe<SlidePlayerSignal>(this);
            m_EventManager.Unsubscribe<StopSlidePlayerSignal>(this);
            m_EventManager.Unsubscribe<StartPlayerMoveSignal>(this);
            m_EventManager.Unsubscribe<RotatePlayerSignal>(this);
            m_EventManager.Unsubscribe<ChangeSkinSignal>(this);
        }
    }
}

