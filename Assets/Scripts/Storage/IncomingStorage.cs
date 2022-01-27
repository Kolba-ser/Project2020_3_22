using Assets.Scripts.Storage.Base;
using Assets.Scripts.Storage.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Storage
{
    public class IncomingStorage : StorageBase, IIncomingStorage
    {
        [SerializeField]
        private int _capacity;

        public override int Capacity => _capacity;
    
    }
}
