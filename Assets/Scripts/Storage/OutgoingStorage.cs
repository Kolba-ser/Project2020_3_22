using UnityEngine;
using Assets.Scripts.Storage.Base;
using Assets.Scripts.Storage.Interfaces;

namespace Assets.Scripts
{
    class OutgoingStorage : StorageBase, IOutgoingStorage
    {

        [SerializeField]
        private int _capacity;
        public override int Capacity => _capacity;


    }
}
