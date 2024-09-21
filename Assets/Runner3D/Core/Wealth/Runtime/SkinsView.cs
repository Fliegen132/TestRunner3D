using Core.Skin;
using UnityEngine;

namespace Core.Wealth
{
    public class SkinsView : MonoBehaviour
    {

        [SerializeField] private Skin.Skin[] m_Skins;
        private int m_Index;

        public int Index => m_Index;
        public Skin.Skin[] Skins => m_Skins;
    }
}
