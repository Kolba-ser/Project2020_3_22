using Assets.Scripts.System;
using Assets.Scripts.System.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Product.Base
{
    public abstract class ProductBase : MonoBehaviour, IMovable
    {
        [SerializeField]
        private ResourceType _type;

        public GameObject Prefab;
        public float CreationTime;
        public AnimationCurve Speed;
        
        public Transform TargetPoint;

        public ResourceType Type => _type;
        public Vector3 To => TargetPoint.position;
        public Transform Model => transform;
        public AnimationCurve Curve => Speed;

    }
    public enum ResourceType
    {
        A,
        B,
        C
    }
}
