using Events;
using System.Collections;
using UnityEngine;

namespace Core.Player
{
    public class CheckSides : MonoBehaviour
    {
        [SerializeField] private float m_RayLength = 5f; 
        [SerializeField] private float m_Offset = 1f;

        private const int m_BaseAngle = 90;
        private EventManager m_EventManager;
        private bool m_IsRayHit = false;
        private bool m_Enable = true;

        public void Init(EventManager eventManager)
        { 
            m_EventManager = eventManager;
        }

        private void Update()
        {
            if (!m_Enable)
            {
                return;
            }

            Vector3 position = transform.position;

            Vector3 leftRayStart = position 
                + (Quaternion.Euler(0, transform.eulerAngles.y, 0) 
                * new Vector3(-m_Offset, 0, 0)) + new Vector3(0, 2, 0);

            Vector3 rightRayStart = position
                + (Quaternion.Euler(0, transform.eulerAngles.y, 0)
                * new Vector3(m_Offset, 0, 0)) + new Vector3(0, 2, 0);

            CheckRaycast(leftRayStart, -m_BaseAngle);
            CheckRaycast(rightRayStart, m_BaseAngle);
        }

        private void CheckRaycast(Vector3 rayStart, float angle)
        {
            RaycastHit hit;

            if (Physics.Raycast(rayStart, Vector3.down, out hit, m_RayLength))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Floor") && !m_IsRayHit)
                {
                    StartCoroutine(Rotate(angle));
                }
            }
            else
            {
                m_IsRayHit = false;
            }
        }

        private IEnumerator Rotate(float angle)
        {
            StartCoroutine(WaitEnable());
            m_IsRayHit = true;
            m_Enable = false;
            m_EventManager.TriggerEvenet<ChangePointsSignal>();
            yield return new WaitForSeconds(0.4f);
            m_EventManager.TriggerEvenet<RotatePlayerSignal, float>(angle);
        }

        private IEnumerator WaitEnable()
        {
            yield return new WaitForSeconds(2f);
            m_Enable = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector3 position = transform.position;

            Vector3 leftRayStart = position + (Quaternion.Euler(0, transform.eulerAngles.y, 0) 
                * new Vector3(-m_Offset, 0, 0)) + new Vector3(0, 2, 0);

            Vector3 rightRayStart = position + (Quaternion.Euler(0, transform.eulerAngles.y, 0) 
                * new Vector3(m_Offset, 0, 0)) + new Vector3(0, 2, 0);

            Gizmos.DrawLine(leftRayStart, leftRayStart + Vector3.down * m_RayLength);
            Gizmos.DrawLine(rightRayStart, rightRayStart + Vector3.down * m_RayLength);
        }
    }
}
