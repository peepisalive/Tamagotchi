using System.Collections.Generic;
using Save;

namespace Events
{
    public sealed class SaveDataEvent
    {
        public List<SaveData> SaveData;
        public bool IsAsync;
    }
}