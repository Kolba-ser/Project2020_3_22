using Assets.Scripts.Product;
using Assets.Scripts.Product.Base;

namespace Assets.Scripts.Storage.Interfaces
{
    public interface IIncomingStorage
    {
        public bool TryAddResource(ProductBase product);
    }
}