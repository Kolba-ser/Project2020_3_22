using Assets.Scripts.Factory.Base;
using Assets.Scripts.Product.Base;
using Assets.Scripts.Storage.Base;
using System;

namespace Assets.Scripts.Logic
{
    public class Loader : IDisposable
    {
        private FactoryBase[] _factories;
        private StorageBase _storage;

        public Loader(FactoryBase[] factories, StorageBase outgoing)
        {
            _factories = factories;
            _storage = outgoing;

            foreach (var factory in factories)
            {
                factory.EndCreationEvent += OnEndCreation;
            }
        }

        public void Dispose()
        {
            foreach (var factory in _factories)
            {
                factory.EndCreationEvent -= OnEndCreation;
            }
        }

        private void OnEndCreation(ProductBase product)
        {
            _storage.TryAddResource(product);
        }
    }
}
