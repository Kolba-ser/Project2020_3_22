using Assets.Scripts.System.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.System
{
    public class UpdateSystem : MonoBehaviour
    {
        private List<IUpdatable> _updatables = new List<IUpdatable>();

        public void AddUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }
        public void Update()
        {
            foreach (var updatable in _updatables)
            {
                updatable.OnUpdate();
            }  
        }
        public void RemoveUpdatable(IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }
    }
}
