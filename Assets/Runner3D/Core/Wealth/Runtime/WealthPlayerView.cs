using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Wealth 
{
    public class WealthPlayerView : MonoBehaviour
    {
        [SerializeField] private Color m_StartColor;
        [SerializeField] private Color m_TargetColor;
        [SerializeField] private Image m_Fill;
        [SerializeField] private Slider m_Slider;
        [SerializeField] private TextMeshProUGUI m_WealthText;

        public void ChangeColor()
        {
            m_Fill.color = Color.Lerp(m_StartColor, m_TargetColor, m_Slider.value / m_Slider.maxValue);
        }

        public void ChangeValue(int value)
        {
            m_Slider.value += value;
            ChangeColor();
        }

        public void ChangeText(string text)
        {
            if (text == m_WealthText.text)
            {
                return;
            }

            m_WealthText.text = text;
        }
    }

}

