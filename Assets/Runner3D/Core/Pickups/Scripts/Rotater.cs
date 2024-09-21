using UnityEngine;

namespace Core.Item
{
    public class Rotater : MonoBehaviour
    {
        private const float speed = 50;
        private float m_Angle;

        private void Update()
        {
            m_Angle += speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, transform.rotation.y + m_Angle, 0);
        }
    }
}

