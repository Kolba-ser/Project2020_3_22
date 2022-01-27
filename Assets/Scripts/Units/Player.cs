using Assets.Scripts.Product;
using Assets.Scripts.Product.Base;
using Assets.Scripts.Storage.Interfaces;
using Assets.Scripts.Units.Base;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class Player : PlayerBase
    {
        [SerializeField]
        private Transform _inventoryPoint;
        [SerializeField]
        private float _productSpacing;
        [SerializeField]
        private float _timeToInteract;

        private float _progress;

        private GameObject[] _inventoryCells;
        
        private void Start()
        {
            _inventoryCells = new GameObject[Capacity];
            

            for (int i = 0; i < Capacity; i++)
            {
                
                var go = new GameObject();
                go.transform.SetParent(_inventoryPoint);
                go.name = "Cell";
                _inventoryCells[i] = go;

                var position = new Vector3
                    (
                    _inventoryPoint.position.x,
                    _inventoryPoint.position.y + _productSpacing * i,
                    _inventoryPoint.position.z
                    );

                _inventoryCells[i].transform.position = position;
            }
        }

        public void OnTriggerStay(Collider other)
        {
            if (_progress < _timeToInteract)
                return;

            if (other.TryGetComponent<IOutgoingStorage>(out var outgoing))
            {
                if (Inventory.Count < Capacity && outgoing.TryGetResource(out var product))
                {

                    PullToInventory(product);
                }
            }

            if (other.TryGetComponent<IIncomingStorage>(out var incoming))
            {

                if (Inventory.Count > 0)
                {
                    var product = RemoveFromInventory();
                    if (!incoming.TryAddResource(product))
                    {
                        Debug.Log("Убрал обратно в инвентраь");
                        PullToInventory(product);
                    }
                }

            }
        }

        protected override void PullToInventory(ProductBase product)
        {
            var targetCell = _inventoryCells[Inventory.Count].transform;
            product.transform.SetParent(targetCell);

            product.TargetPoint = targetCell;

            product.Prefab.name = $"InInventory {product.Type}";
            Inventory.Push(product);

            _progress = 0;
        }
        protected override ProductBase RemoveFromInventory()
        {
            var removed = Inventory.Pop();
            removed.transform.SetParent(null);
            removed.transform.name = $"RemovedByPlayer {removed.Type}";
            _progress = 0;
            return removed;

        }

        public override void OnUpdate()
        {
            _progress = _progress < _timeToInteract
                ? _progress + Time.deltaTime
                : _timeToInteract;


            var targetDirection = new Vector3
                (
                -_joystick.Direction.x,
                0,
                -_joystick.Direction.y
                );

            transform.position += targetDirection * _speed * Time.deltaTime;
        }

    }
}
