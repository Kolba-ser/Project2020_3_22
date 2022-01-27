using Assets.Scripts.Data;
using Assets.Scripts.Product;
using Assets.Scripts.Product.Base;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Factory.Base
{
    public abstract class FactoryBase : MonoBehaviour
    {
        public FactorySettings Settings;
        public FactoryState ActiveState { get; private set; }
        public float LaunchProgress;

        public Stack<ProductBase> Container = new Stack<ProductBase>();
        public List<ResourceType> ProductsList = new List<ResourceType>();

        public delegate void ProductionHandler(ProductBase product);
        public abstract event ProductionHandler EndCreationEvent;

        public delegate void StateHandler(FactoryBase factory);
        public event StateHandler StateChangedEvent;
        public abstract void CreateProduct();
        
        public void ChangeState(FactoryState state)
        {
            ActiveState = state;
            StateChangedEvent?.Invoke(this);
        }

        protected virtual void FillList() { }
    }

    public enum FactoryState
    {
        TryStart,
        ReadyToWork,
        InProcess,
        RequireResource,
        Deactivate,
        LackOfResourse,
        OverflowStorage,
    }
}
