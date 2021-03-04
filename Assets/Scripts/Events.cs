using System;
using ExtraTools;

namespace GGJ21
{
    public class Events : Singleton<Events>
    {
        public Action<ItemData> pickedUp;
        public Action<ItemData> removeItem;
        public Action<string> displayMessage;
        public Action<bool> pause;
        public Action switchPressed;
        public Action bookPressed;
        public Action<string> addLog;
        public Action<Endings> end;
        public Action gotTheBlood;
        public Action resetDaggerPosition;
        public Action<bool> gotDagger;
        public Action<bool> gotLeech;
        public Action useDagger;
        public Action pickUpDagger;
        public Action useLeech;
        public Action resetLeechPosition;
    }
}