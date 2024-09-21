using Core.Wealth;
using System;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private CheckSides m_Sides;
        [SerializeField] private TriggerEvent m_PickupCollector;
        [SerializeField] private SkinsView m_WealthSkins;
        [SerializeField] private WealthPlayerView m_WealthSlider;

        private const float m_RotateSpeed = 20f;
        private const float m_RotateSideSpeed = 60f;
        private const float m_Angle = 30;
        private const float m_MoveSpeed = 5f;
        private const float m_SlideSpeed = 10f;

        private Rigidbody m_Rigidbody;
        private GameObject m_Armature;

        private float m_CurrentAngle;
        private bool m_IsSlide;
        private bool m_IsMoving;
        private bool m_TrueRotate = true;
        private bool m_CanSlide = true;
        private Action m_CurrentRotateState;

        public bool IsMoving
        {
            get
            {
                return m_IsMoving;
            }
            set
            {
                m_IsMoving = value;
            }
        }
        public bool IsSlide
        {
            get
            {
                return m_IsSlide;
            }
            set
            {
                m_IsSlide = value;
            }
        }
        public CheckSides Sides => m_Sides;
        public TriggerEvent PickupCollector => m_PickupCollector;
        public SkinsView WealthSkins => m_WealthSkins;
        public WealthPlayerView WealthSlider => m_WealthSlider;

        private void Awake()
        {
            m_Armature = transform.GetChild(0).gameObject;
            m_Rigidbody = GetComponent<Rigidbody>();
            m_CurrentRotateState = SetZeroRotate;
        }

        //TODO state pattern
        private void Update()
        {
            m_CurrentRotateState?.Invoke();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void SetZeroRotate()
        {
            if (!m_IsSlide)
            {
                if (m_Armature.transform.localRotation == Quaternion.Euler(new Vector3(0, 0, 0)))
                {
                    return;
                }

                m_Armature.transform.localRotation = GetQuaternion(0);
            }
        }

        private void Rotate()
        {
            if (!m_TrueRotate)
            {
                transform.rotation = GetQuaternionBase(m_CurrentAngle);
                if (m_CanSlide)
                { 
                    m_CanSlide = false;
                }
                if (Mathf.Abs(transform.eulerAngles.y - m_CurrentAngle) < 0.01)
                {
                    m_CurrentRotateState = SetZeroRotate;
                    m_TrueRotate = true;
                    ActiveCanSlide();
                    Debug.Log(m_TrueRotate);
                }
            }
        }

        private void ActiveCanSlide()
        {
            m_CanSlide = true;
        }

        private void Move()
        {
            if (m_IsMoving)
            {
                Vector3 movementDirection = transform.forward * m_MoveSpeed * Time.fixedDeltaTime;

                m_Rigidbody.MovePosition(m_Rigidbody.position + movementDirection);
            }
        }
       
        public void SetRotateOrigin(float angle)
        { 
            m_CurrentAngle += angle;
            m_TrueRotate = false;
            m_CurrentRotateState = Rotate;
        }

        public void Slide(Vector3 direction)
        {
            if (!m_CanSlide)
            {
                return;
            }

            if (direction != Vector3.zero)
            {
                Vector3 moveVelocity = direction.normalized * m_SlideSpeed;
                Vector3 rightMovement = moveVelocity.x * transform.right;
                m_Rigidbody.linearVelocity = rightMovement;

                if (direction != Vector3.zero)
                {
                    if (direction.x < 0)
                    {
                        m_Armature.transform.localRotation = GetQuaternion(-m_Angle);
                    }
                    else if (direction.x > 0)
                    {
                        m_Armature.transform.localRotation = GetQuaternion(m_Angle);
                    }
                }
            }
        }

        public void StopSlide()
        {
            m_Rigidbody.linearVelocity = Vector3.zero;
        }

        private Quaternion GetQuaternionBase(float angle)
        {
            return Quaternion.Lerp(transform.rotation,
                       Quaternion.Euler(new Vector3(0, angle, 0)), Time.deltaTime * m_RotateSpeed);
        }

        private Quaternion GetQuaternion(float angle)
        {
            return Quaternion.Lerp(m_Armature.transform.localRotation,
                       Quaternion.Euler(new Vector3(0, angle, 0)), Time.deltaTime * m_RotateSpeed);
        }
    }
}
