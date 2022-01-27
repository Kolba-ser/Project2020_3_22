using Assets.Scripts.System.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.System
{
    class MoveSystem : IUpdatable, IMoveSystem
    {
        private List<IMovable> _movables = new List<IMovable>();

        public void AddMovable(IMovable movable)
        {
            _movables.Add(movable);
        }
        public void RemoveMovable(IMovable movable)
        {
            _movables.Remove(movable);
        }
        public void OnUpdate()
        {
            foreach (var movable in _movables)
            {
                try
                {
                    var distance = Vector3.Distance(movable.Model.position, movable.To);
                    var speed = movable.Curve.Evaluate(distance) * Time.deltaTime;

                    movable.Model.position = Vector3.Lerp
                        (movable.Model.position,
                        movable.To,
                        speed
                        );
                }
                catch (MissingReferenceException)
                {
                    RemoveMovable(movable);
                    return;
                }
            }
        }
    }
}
