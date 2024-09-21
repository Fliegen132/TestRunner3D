using UnityEngine;

namespace Core.Canvas
{
    public class PopupCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject m_LoseWindow;
        [SerializeField] private GameObject m_WinWindow;

        public void ActiveLoseWindow()
        {
            m_LoseWindow.SetActive(true);
        }

        public void ActiveWinWindow()
        {
            m_WinWindow.SetActive(true);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
