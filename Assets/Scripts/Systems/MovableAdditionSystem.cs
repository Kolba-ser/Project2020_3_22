using Assets.Scripts.Factory.Base;
using Assets.Scripts.Product.Base;
using Assets.Scripts.System.Interfaces;
using System;

namespace Assets.Scripts.System
{
    public class MovableAdditionSystem : IDisposable
    {
        private FactoryBase[] _factories;
        private IMoveSystem _system;

        public MovableAdditionSystem(FactoryBase[] factories, IMoveSystem system)
        {
            _factories = factories;
            _system = system;

            foreach (var factory in _factories)
            {
                factory.EndCreationEvent += OnEndCreation;
            }
        }

        private void OnEndCreation(ProductBase product)
        {
            _system.AddMovable(product);
        }

        public void Dispose()
        {
            foreach (var factory in _factories)
            {
                factory.EndCreationEvent -= OnEndCreation;
            }
        }
    }
}
