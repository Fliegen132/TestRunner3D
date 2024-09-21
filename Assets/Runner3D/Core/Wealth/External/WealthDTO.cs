
using Events;

namespace Core.Wealth
{
    public class WealthDTO
    {
        private const int m_StartWealth = 40;
        private EventManager m_EventManager;
        private int m_Wealth;

        public WealthDTO(EventManager eventManager)
        {
            m_EventManager = eventManager;
            m_Wealth = m_StartWealth;
        }

        public void ChangeWealth(int wealth)
        {
            m_Wealth += wealth;
            m_EventManager.TriggerEvenet<ChangeSkinSignal>();
            if (m_Wealth <= 0)
            {
                Lose();
            }
        }

        private void Lose()
        {
            m_EventManager.TriggerEvenet<OpenLosePopupSignal>();
            m_EventManager.TriggerEvenet<StopPlayerMoveSignal>();
        }

        public int GetWealth() => m_Wealth;
    }
}
