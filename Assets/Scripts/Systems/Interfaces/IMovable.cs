using UnityEngine;

namespace Assets.Scripts.System.Interfaces
{
    public interface IMovable
    {
        public Vector3 To { get; }

        public Transform Model { get; }

        public AnimationCurve Curve { get; }
    }
}
