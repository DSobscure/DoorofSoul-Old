using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Library.General.Events.Managers;

namespace DoorofSoul.Library.General.Events
{
    public class PlayerEventManagers
    {
        protected Player player;
        public PlayerSceneEventManager PlayerSceneEventManager { get; protected set; }

        public PlayerEventManagers(Player player)
        {
            this.player = player;
            PlayerSceneEventManager = new PlayerSceneEventManager(player);
        }
    }
}
