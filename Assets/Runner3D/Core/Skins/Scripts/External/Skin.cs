using System;
using UnityEngine;

namespace Core.Skin
{
    [Serializable]
    public class Skin
    {
        [SerializeField] private int m_Value;
        [SerializeField] private GameObject m_Skin;
        [SerializeField] private string m_Text; 
        [SerializeField] private int m_Level; 
        public int Value => m_Value;
        public GameObject GameObject => m_Skin;
        public string Text => m_Text;
        public int Level => m_Level;
    }
}

