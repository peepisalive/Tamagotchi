using System.Collections.Generic;
using Save.State;
using System;

namespace Components
{
    public struct SaveDataLoadedComponent
    {
        private Dictionary<Type, IStateHolder> _stateHolders;

        public SaveDataLoadedComponent(Dictionary<Type, IStateHolder> stateHolders)
        {
            _stateHolders = stateHolders;
        }

        public T Get<T>() where T : IStateHolder
        {
            var type = typeof(T);

            if (_stateHolders.ContainsKey(type))
                return (T)_stateHolders[type];

            return default(T);
        }
    }
}