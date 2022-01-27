using Assets.Scripts.Factory.Base;
using Assets.Scripts.Product.Base;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    class FactoryB : FactoryBase
    {
        public override event ProductionHandler EndCreationEvent;

        private void Start()
        {
            FillList();
        }

        public override void CreateProduct()
        {
            ChangeState(FactoryState.RequireResource);

            if (ProductsList.Count == 0)
            {
                ChangeState(FactoryState.ReadyToWork);
                var createdProduct = RecycleResource();
                createdProduct.transform.position = transform.position;
                EndCreationEvent?.Invoke(createdProduct);
                FillList();
            }

        }
        private ProductBase RecycleResource()
        {
            var iterations = Container.Count;

            for (int i = 0; i < iterations; i++)
            {
                var removed = Container.Pop();
                removed.name = $"Removed {removed.Type}";
                GameObject.Destroy(removed.Prefab);
            }
            ProductsList.Clear();

            return Instantiate(Settings.ManufacturedProduct);
        }
        protected override void FillList()
        {
            foreach (var product in Settings.RequiredResources)
            {
                for (int i = 0; i < product.Amount; i++)
                {
                    ProductsList.Add(product.Resource.Type);
                }
            }

        }
    }
}
