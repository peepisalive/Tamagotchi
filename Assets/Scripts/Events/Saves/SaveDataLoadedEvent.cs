using System.Collections.Generic;
using Save.State;
using System;

namespace Events.Saves
{
    public sealed class SaveDataLoadedEvent
    {
        private Dictionary<Type, IStateHolder> _stateHolders;

        public SaveDataLoadedEvent(Dictionary<Type, IStateHolder> stateHolders)
        {
            _stateHolders = stateHolders;
        }

        public T Get<T>() where T : IStateHolder
        {
            var type = typeof(T);

            if (_stateHolders.ContainsKey(type))
                return (T)_stateHolders[type];

            return default;
        }
    }
}