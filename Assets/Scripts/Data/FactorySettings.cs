using Assets.Scripts.Product;
using Assets.Scripts.Product.Base;
using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(menuName = "Data/FactorySettings")]
    public class FactorySettings : ScriptableObject
    {
        public ProductBase ManufacturedProduct;

        public Required[] RequiredResources;


        public bool NeedResources => RequiredResources.Length > 0;
    }

    [Serializable]
    public struct Required
    {
        public ProductBase Resource;
        public int Amount;
    }


}