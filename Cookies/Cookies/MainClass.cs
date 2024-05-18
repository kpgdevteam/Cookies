using System.Collections.Generic;
using System.Linq;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;

namespace Cookies
{
    public class MainClass
    {
        public const string Author = "Cookies";
        public const string Name = "Cookies";
        public const string Version = "1.0.0";
        public const string Description = "Cookies Random Plugins";

        private const int ServerLimit = 4;
        private readonly Queue<(Player, int)> playerQueue = new Queue<(Player, int)>();
        private int nextQueueNumber = 1;

        [PluginEntryPoint(Name, Version, Description, Author)]
        public void OnEnabled()
        {
            Log.Info("Cookies plugin loaded. Written by Cookies");
            EventManager.RegisterEvents(this);
        }

        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoined(Player player)
        {
            int onlinePlayerCount = Server.Count;
            if (onlinePlayerCount >= ServerLimit)
            {
                int queueNumber = nextQueueNumber++;
                playerQueue.Enqueue((player, queueNumber));
                player.SetRole(RoleTypeId.Overwatch);
                player.SendBroadcast($"The server is currently full. You have been added to the queue. Your queue number is {queueNumber}.", 10);
                player.ReceiveHint($"Queue Status {queueNumber}/{playerQueue}", 1F);
            }
            else
            {
                player.SetRole(RoleTypeId.Spectator);
            }
        }

        [PluginEvent(ServerEventType.PlayerLeft)]
        public void OnPlayerLeft(Player player)
        {
            if (playerQueue.Count > 0)
            {
                (Player nextPlayer, int queueNumber) = playerQueue.Dequeue();
                nextPlayer.SetRole(RoleTypeId.Spectator);
                nextPlayer.SendBroadcast($"It's your turn to play! You have been spawned into the game. Your queue number was {queueNumber}.", 10);
            }
        }
    }
}