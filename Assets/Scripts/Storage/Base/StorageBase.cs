using Assets.Scripts.Product.Base;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Storage.Base
{
    public abstract class StorageBase : MonoBehaviour
    {
        public abstract int Capacity { get; }

        protected Dictionary<ResourceType, Stack<ProductBase>> Storage =
            new Dictionary<ResourceType, Stack<ProductBase>>();

        private Dictionary<ResourceType, Transform> _containers =
            new Dictionary<ResourceType, Transform>();

        public delegate void ResourceHandler(ResourceType type);
        public delegate void StorageHandler();

        public event ResourceHandler OverflowEvent;
        public event ResourceHandler LackOfResourceEvent;
        public event ResourceHandler AdditionEvent;
        public event ResourceHandler WithdrawlEvent;

        public event StorageHandler AllResourcesHaveRunOutEvent;

        public bool TryAddResource(ProductBase product)
        {
            var type = product.Type;

            if (!CanAdd(type))
                return false;

            product.transform.SetParent(_containers[type]);
            Storage[type].Push(product);
            AdditionEvent?.Invoke(type);

            product.TargetPoint = transform;

            CanAdd(type);

            return true;
        }
        public bool TryGetResource(out ProductBase product)
        {
            product = null;

            foreach (var products in Storage.Values)
            {
                if (products.Count > 0)
                {
                    product = products.Pop();
                    product.transform.SetParent(null);
                    WithdrawlEvent?.Invoke(product.Type);
                    return true;
                }
            }

            AllResourcesHaveRunOutEvent?.Invoke();
            return false;
        }
        public bool TryGetResource(ResourceType type, out ProductBase product)
        {
            product = null;

            if (!CanGet(type))
            {
                return false;
            }

            product = Storage[type].Pop();
            product.transform.SetParent(null);
            WithdrawlEvent?.Invoke(type);
            return true;
        }

        private bool CanGet(ResourceType type)
        {

            if (!Storage.ContainsKey(type) || Storage[type].Count == 0)
            {
                LackOfResourceEvent?.Invoke(type);
                return false;
            }

            return true;
        }
        public bool CanAdd(ResourceType type)
        {
            if (!Storage.ContainsKey(type))
            {
                Storage[type] = new Stack<ProductBase>();

                var go = new GameObject();
                var container = go.transform;
                container.name = type.ToString();
                container.SetParent(transform);
                _containers[type] = container;

            }

            if (Storage[type].Count < Capacity)
            {
                return true;
            }

            OverflowEvent?.Invoke(type);
            return false;
        }

    }
}