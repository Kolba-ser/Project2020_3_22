using Assets.Scripts.Factory;
using Assets.Scripts.Product;
using Assets.Scripts.Product.Base;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Presenter.Component
{
    [Serializable]
    public struct UISettings
    {
        public ResourceType Factory;
        public Text TextState;
    }
}