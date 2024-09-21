using UnityEngine;

namespace Core.Door
{
    public class EndDoor : MonoBehaviour
    {
        [SerializeField] private int m_Level;
        [SerializeField] private Animator m_Animator;

        public bool CanOpen(int level)
        {
            Debug.Log("Level " + level);
            if (m_Level < level)
            {
                m_Animator.SetBool("Open", true);
                return true;
            }
            else
            { 
                return false;
            }
        }

    }
}

