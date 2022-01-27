using Assets.Scripts.Factory.Base;
using Assets.Scripts.Product;

namespace Assets.Scripts.Factory
{
    class FactoryA : FactoryBase
    {
        public override event ProductionHandler EndCreationEvent;

        public override void CreateProduct()
        {
            ChangeState(FactoryState.InProcess);
            var createdProduct = Instantiate(Settings.ManufacturedProduct);
            createdProduct.transform.position = transform.position;
            ChangeState(FactoryState.ReadyToWork);
            EndCreationEvent?.Invoke(createdProduct);
        }
    }
}
