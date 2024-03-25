using System.Collections.Generic;
using Save;

namespace Events.Saves
{
    public sealed class SaveDataEvent
    {
        public List<SaveData> SaveData;
        public bool IsAsync;
    }
}