using UnityEngine;

namespace Core.Point
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;

        public void Trigger()
        {
            m_Animator.SetBool("Open", true);
        }
    }
}

