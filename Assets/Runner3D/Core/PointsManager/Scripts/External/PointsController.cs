using Events;
using OrderMaster;
using Services;
using System.Drawing;
using UnityEngine;

namespace Core.Points
{
    [Order(3)]
    public class PointsController : MonoBehaviour, IControlEntity
    {
        [SerializeField] private GameObject[] m_Points;
        private EventManager m_EventManager;
        private int m_CurrentPoints;

        public void PreInit()
        {
            foreach (var point in m_Points) 
            {
                point.SetActive(false);
            }
            m_EventManager = ServiceLocator.Current.Get<EventManager>();
            m_CurrentPoints = 0;
            m_Points[m_CurrentPoints].SetActive(true);
            m_EventManager.Subscribe<ChangePointsSignal>(this, ChangePoints);
        }

        public void Initing()
        {
        }

        public void PostInit()
        {
        }

        private void ChangePoints() 
        {
            m_Points[m_CurrentPoints].SetActive(false);
            m_CurrentPoints++;
            m_Points[m_CurrentPoints].SetActive(true);
        } 
    }
}

