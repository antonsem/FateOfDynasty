using System;
using ExtraTools;

namespace GGJ21
{
    public class Events : Singleton<Events>
    {
        public Action<ItemData> pickedUp;
    }
}