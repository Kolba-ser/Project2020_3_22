using Assets.Scripts.Factory.Base;
using Assets.Scripts.Product.Base;
using Assets.Scripts.Storage.Base;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class FactoryHandler : IDisposable
    {

        private StorageBase _incoming;
        private StorageBase _outgoing;

        private Dictionary<ResourceType, FactoryBase> _resourcesProduced;
        private Dictionary<ResourceType, List<FactoryBase>> _requiredResources;

        public FactoryHandler(StorageBase incoming, StorageBase outgoing,
            Dictionary<ResourceType, FactoryBase> resourcesProduced, Dictionary<ResourceType, List<FactoryBase>> requiredResources)
        {
            _resourcesProduced = resourcesProduced;
            _requiredResources = requiredResources;

            _outgoing = outgoing;
            _incoming = incoming;

            _incoming.LackOfResourceEvent += OnLackOfResources;
            _incoming.AdditionEvent += OnAddition;
            _incoming.AllResourcesHaveRunOutEvent += OnAllResourcesHaveRunOut;

            _outgoing.OverflowEvent += OnOverflow;
            _outgoing.WithdrawlEvent += OnWithdrawl;
        }

        private void OnAllResourcesHaveRunOut()
        {
            foreach (var value in _requiredResources.Values)
            {
                value.ForEach(x => x.ChangeState(FactoryState.RequireResource));
            }
        }

        private void OnWithdrawl(ResourceType type)
        {
            _resourcesProduced[type].ChangeState(FactoryState.TryStart);
        }
        private void OnAddition(ResourceType type)
        {
            if (!_requiredResources.ContainsKey(type))
                return;
            foreach (var factory in _requiredResources[type])
            {
                factory.ChangeState(FactoryState.RequireResource);
            }

        }
        private void OnOverflow(ResourceType type)
        {
            Debug.Log($"Склад переполнен {type}");

            _resourcesProduced[type].ChangeState(FactoryState.OverflowStorage);
        }
        private void OnLackOfResources(ResourceType type)
        {

            foreach (var factory in _requiredResources[type])
            {
                factory.ChangeState(FactoryState.RequireResource);
            }
        }

        public void Dispose()
        {
            _incoming.LackOfResourceEvent -= OnLackOfResources;
            _incoming.AdditionEvent -= OnAddition;
            _outgoing.OverflowEvent -= OnOverflow;
            _outgoing.WithdrawlEvent -= OnWithdrawl;
        }
    }
}
