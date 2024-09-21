using Services;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets
{
    public class AssetLoader : IService
    {
        public T Load<T>(string key) where T : UnityEngine.Object
        {
            var opHandle = Addressables.LoadAssetAsync<T>(key);
            opHandle.WaitForCompletion();

            if (opHandle.Status == AsyncOperationStatus.Failed)
            {
                throw new Exception($"Не удалось загрузить ассет {key}");
            }

            T obj = opHandle.Result;
            return obj;
        }

        public T InstantiateSync<T>(GameObject handle, Transform parent) where T : Component
        {
            GameObject obj = handle;
            var result = UnityEngine.Object.Instantiate(obj, parent);

            if (result.GetComponent<T>())
            {
                return result.GetComponent<T>();
            }
            else
            {
                throw new Exception($"{nameof(T)} не найден");
            }
        }

        public T InstantiateSync<T>(GameObject handle, Vector3 position, Quaternion rotation) where T : Component
        {
            GameObject obj = handle;
            var result = UnityEngine.Object.Instantiate(obj, position, rotation);

            if (result.GetComponent<T>())
            {
                return result.GetComponent<T>();
            }
            else
            {
                throw new Exception($"{nameof(T)} не найден");
            }
        }

        public T InstantiateSync<T>(GameObject handle, Vector3 position, Quaternion rotation, Transform parent) where T : Component
        {
            GameObject obj = handle;
            var result = UnityEngine.Object.Instantiate(obj, position, rotation, parent);

            if (result.GetComponent<T>())
            {
                return result.GetComponent<T>();
            }
            else
            {
                throw new Exception($"{nameof(T)} не найден");
            }
        }
    }
}
