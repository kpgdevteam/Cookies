using PluginAPI.Core;
using PluginAPI.Core.Attributes;

namespace Cookies
{
    public class MainClass
    {
        public const string Author = "Cookies";
        public const string Name = "Cookies";
        public const string Version = "1.0.0";
        public const string Description = "Cookies Random Plugins";

        [PluginEntryPoint(Name, Version, Description, Author)]
        public void OnEnabled()
        {
            Log.Info("Cookies plugin loaded. Written by Cookies");
        }
    }
}
