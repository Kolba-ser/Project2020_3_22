using Assets.Scripts.Product.Base;
using Assets.Scripts.System.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units.Base
{
    public abstract class PlayerBase : MonoBehaviour, IUpdatable
    {
        [SerializeField]
        protected int Capacity;
        [SerializeField]
        protected Joystick _joystick;
        [SerializeField]
        protected float _speed;

        protected Stack<ProductBase> Inventory = new Stack<ProductBase>();

        public abstract void OnUpdate();

        protected abstract void PullToInventory(ProductBase product);
        protected abstract ProductBase RemoveFromInventory();

    }
}