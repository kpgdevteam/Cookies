using CommandSystem;
using System;
using PluginAPI.Core;

namespace Cookies.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class Blackout : ICommand
    {
        public string Command => "blackout";
        public string[] Aliases { get; } = { "cookies" };

        public string Description => "Blackout The Entire Faculty";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.SetGroup))
            {
                response = "System Administrator Only Command";
                return false;
            }
            else
            {
                Facility.TurnOffAllLights();
                response = "All Lights Have Been Turned OFF!";
                return true;
            }
        }
    }
}
