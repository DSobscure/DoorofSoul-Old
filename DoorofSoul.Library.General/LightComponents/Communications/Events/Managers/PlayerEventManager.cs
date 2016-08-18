using DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers;
using DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Player;
using DoorofSoul.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Managers
{
    public class PlayerEventManager
    {
        private readonly Dictionary<PlayerEventCode, PlayerEventHandler> eventTable;
        protected readonly Player player;
        public InformDataResolver InformDataResolver { get; protected set; }

        internal PlayerEventManager(Player player)
        {
            this.player = player;
            InformDataResolver = new InformDataResolver(player);
            eventTable = new Dictionary<PlayerEventCode, PlayerEventHandler>
            {
                { PlayerEventCode.AnswerEvent, new AnswerEventResolver(player) },
                { PlayerEventCode.InformData, InformDataResolver },
            };
        }

        public void Operate(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LibraryInstance.ErrorFormat("Player Event Error: {0} from PlayerID: {1}", eventCode, player.PlayerID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Player Event:{0} from PlayerID: {1}", eventCode, player.PlayerID);
            }
        }

        internal void SendEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            player.PlayerCommunicationInterface.SendEvent(eventCode, parameters);
        }

        public void ErrorInform(string title, string message)
        {
            player.PlayerCommunicationInterface.ErrorInform(title, message);
        }
    }
}
