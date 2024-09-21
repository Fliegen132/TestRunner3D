using System;
using System.Linq;
using UnityEngine;

namespace OrderMaster
{
    public class StarterOrderMaster : MonoBehaviour
    {
        [Obsolete]
        private void Awake()
        {
            Init();
        }

        [Obsolete]
        private void Init()
        {
            var allControllers = FindObjectsOfType<MonoBehaviour>()
                .OfType<IControlEntity>()
                .Select(init => new
                {
                    Instance = init,
                    Order = GetControllerOrder(init.GetType())
                })
                .OrderBy(order => order.Order)
                .ToList();

            foreach (var controller in allControllers)
            {
                Debug.Log(controller.Instance + " " + controller.Order);  
                controller.Instance.PreInit();
                controller.Instance.Initing();
                controller.Instance.PostInit();
            }
        }

        private int GetControllerOrder(Type type)
        {
            var attribute = (OrderAttribute)Attribute.GetCustomAttribute(type, typeof(OrderAttribute));

            if (attribute != null)
            {
                return attribute.OrderID;
            }
            else
            {
                return int.MaxValue;
            }
        }
    }
}

