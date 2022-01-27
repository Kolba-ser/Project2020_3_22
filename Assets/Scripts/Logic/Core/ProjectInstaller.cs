using Assets.Scripts.Factory;
using Assets.Scripts.Factory.Base;
using Assets.Scripts.Presenter;
using Assets.Scripts.Presenter.Component;
using Assets.Scripts.Product;
using Assets.Scripts.Product.Base;
using Assets.Scripts.Storage;
using Assets.Scripts.Storage.Base;
using Assets.Scripts.System;
using Assets.Scripts.Units;
using Assets.Scripts.Units.Base;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic.Core
{
    [RequireComponent(typeof(UpdateSystem))]
    public class ProjectInstaller : MonoBehaviour
    {
        [SerializeField]
        private FactoryBase[] _factories;
        [SerializeField]
        private StorageBase _incoming;
        [SerializeField]
        private StorageBase _outgoing;
        [SerializeField]
        private PlayerBase _player;
        [SerializeField]
        private List<UISettings> _uISettings;

        private Dictionary<ResourceType, FactoryBase> _resourcesProduced;
        private Dictionary<ResourceType, List<FactoryBase>> _requiredResources;

        public void Start()
        {
            _resourcesProduced = new Dictionary<ResourceType, FactoryBase>();
            _requiredResources = new Dictionary<ResourceType, List<FactoryBase>>();

            foreach (var factory in _factories)
            {
                var type = factory.Settings.ManufacturedProduct.Type;

                _resourcesProduced.Add(type, factory);


                foreach (var item in factory.Settings.RequiredResources)
                {
                    var requiredType = item.Resource.Type;

                    if (!_requiredResources.ContainsKey(requiredType))
                        _requiredResources.Add(requiredType, new List<FactoryBase>());

                    _requiredResources[requiredType].Add(factory);
                }
            }

            var updateSystem = GetComponent<UpdateSystem>();
            var resourceLoader = new ResourceLoader(_incoming, _factories);
            var launcher = new FactoryLauncher(_factories, resourceLoader);
            
            new Loader(_factories, _outgoing);
            new FactoryHandler(_incoming, _outgoing, _resourcesProduced, _requiredResources);
            new StatePresenter(_factories, _uISettings);
            var moveSystem = new MoveSystem();
            new MovableAdditionSystem(_factories, moveSystem);

            updateSystem.AddUpdatable(launcher);
            updateSystem.AddUpdatable(_player);
            updateSystem.AddUpdatable(moveSystem);

        }
    }
}
