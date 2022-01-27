using Assets.Scripts.Factory.Base;
using Assets.Scripts.Product.Base;
using Assets.Scripts.Storage.Base;
using System.Collections.Generic;

namespace Assets.Scripts.Logic
{
    public class ResourceLoader 
    {
        private FactoryBase[] _factories;
        private StorageBase _incoming;

        public ResourceLoader(StorageBase incoming, FactoryBase[] factories)
        {
            _incoming = incoming;
            _factories = factories;
        }


        public void OnRequestResource(FactoryBase factory, out bool isSuccesfull)
        {
            var copyList = new List<ResourceType>(factory.ProductsList);

            if (!factory.Settings.NeedResources)
            {
                isSuccesfull = true;
                return;
            }

            foreach (var type in copyList)
            {
                if (_incoming.TryGetResource(type, out var product))
                {
                    factory.Container.Push(product);
                    factory.ProductsList.Remove(type);
                }
            }
               
            if(factory.ProductsList.Count == 0)
            {
                isSuccesfull = true;
                return;
            }

            isSuccesfull = false;
        }
    }
}
