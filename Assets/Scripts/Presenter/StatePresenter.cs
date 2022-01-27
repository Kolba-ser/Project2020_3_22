using Assets.Scripts.Factory.Base;
using Assets.Scripts.Presenter.Component;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Presenter
{
    public class StatePresenter : IDisposable
    {
        private FactoryBase[] _factories;
        private List<UISettings> _uISettings;

        public StatePresenter(FactoryBase[] factories, List<UISettings> uISettings )
        {
            _factories = factories;
            _uISettings = uISettings;

            foreach (var factory in _factories)
            {
                factory.StateChangedEvent += OnStateChanged;
            }
        }

        public void Dispose()
        {
            foreach (var factory in _factories)
            {
                factory.StateChangedEvent -= OnStateChanged;
            }
        }

        private void OnStateChanged(FactoryBase factory)
        {
            var data = _uISettings.Find(x => x.Factory.Equals(factory.Settings.ManufacturedProduct.Type));
            data.TextState.text = factory.ActiveState.ToString();
        }
    }
}
