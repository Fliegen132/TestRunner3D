using TMPro;
using UnityEngine;

namespace Core.Wealth
{
    public class WealthView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_WealthCountText;



        public void ChangeValueText(int value)
        {
            m_WealthCountText.text = value.ToString();
        }

        
    }
}
