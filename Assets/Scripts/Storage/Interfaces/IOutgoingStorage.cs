using Assets.Scripts.Product;
using Assets.Scripts.Product.Base;

namespace Assets.Scripts.Storage.Interfaces
{
    public interface IOutgoingStorage
    {
        public bool TryGetResource(out ProductBase product);
    }
}