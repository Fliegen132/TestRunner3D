using Assets;
using Events;
using OrderMaster;
using UnityEngine;
namespace Core.Canvas
{
    [Order(-997)]
    public class LoseViewController : MonoBehaviour, IControlEntity
    {
        private const string m_CanvasName = "LoseView";

        private EventManager m_EventManager;
        private AssetLoader m_AssetLoader;
        private PopupCanvas m_PopupCanvas;

        public void Initing()
        {
        }

        public void PostInit()
        {
        }

        public void PreInit()
        {
        }

        
    }
}