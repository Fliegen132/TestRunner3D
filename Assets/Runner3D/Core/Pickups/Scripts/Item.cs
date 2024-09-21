using UnityEngine;

namespace Core.Pickable
{
    public class Item : MonoBehaviour, IPickable
    {
        [SerializeField] private int m_Value;

        public int Pickup()
        {
            gameObject.SetActive(false);
            return m_Value;
        }
    }
}

