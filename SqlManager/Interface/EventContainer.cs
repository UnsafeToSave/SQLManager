using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlManager
{
    public static class EventContainer
    {
        static Dictionary<string, EventHandler> Events = new Dictionary<string, EventHandler>();

        public static void Add (string name, EventHandler handler)
        {
            if (Events.ContainsKey(name)) return;
            Events.Add(name, handler);
        }

        public static void Invoke(object sender, string name)
        {
            Events[name]?.Invoke(sender, EventArgs.Empty);
        }
    }
}
