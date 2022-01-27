using Assets.Scripts.Factory.Base;
using Assets.Scripts.System.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class FactoryLauncher : IUpdatable
    {
        private FactoryBase[] _factories = new FactoryBase[1];

        private ResourceLoader _resourceLoader;
        public FactoryLauncher(FactoryBase[] factories, ResourceLoader resourceLoader)
        {
            _factories = factories;
            _resourceLoader = resourceLoader;
        }

        public void OnUpdate()
        {

            foreach (var factory in _factories)
            {
                factory.LaunchProgress -=
                    factory.LaunchProgress > 0 ? Time.deltaTime : 0;

                if (factory.LaunchProgress > 0)
                    continue;

                switch (factory.ActiveState)
                {
                    case FactoryState.ReadyToWork:
                        factory.CreateProduct();
                        factory.LaunchProgress = factory.Settings.ManufacturedProduct.CreationTime;
                        break;
                    case FactoryState.RequireResource:
                        _resourceLoader.OnRequestResource(factory, out bool isSuccesfull);
                        if (isSuccesfull)
                            factory.CreateProduct();
                        break;
                    case FactoryState.TryStart:
                        factory.CreateProduct();
                        factory.LaunchProgress = factory.Settings.ManufacturedProduct.CreationTime;
                        break;
                }
            }
        }
    }
}
