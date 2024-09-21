using UnityEngine;

namespace Core.Canvas
{
    public class CoreCanvas : MonoBehaviour
    {
       public Transform GetTransform() 
       { 
            return transform; 
       }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
