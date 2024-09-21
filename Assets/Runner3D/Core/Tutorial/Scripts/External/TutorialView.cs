using UnityEngine;

namespace Core.Tutorial
{
    public class TutorialView : MonoBehaviour
    {
        public void SetActive(bool active)
        { 
            gameObject.SetActive(active);
        }

        public bool GetActive()
        { 
            return gameObject.activeInHierarchy;
        }
    }
}
