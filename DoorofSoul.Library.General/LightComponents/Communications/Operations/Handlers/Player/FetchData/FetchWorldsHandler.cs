using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Player;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Player.FetchData
{
    public class FetchWorldsHandler : FetchDataHandler
    {
        public FetchWorldsHandler(General.Player player) : base(player, 0)
        {
            
        }

        public override bool Handle(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    IEnumerable<NatureComponents.World> worlds = player.PlayerCommunicationInterface.GetWorlds();
                    foreach(NatureComponents.World world in worlds)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)FetchWorldsResponseParameterCode.WorldID, world.WorldID },
                            { (byte)FetchWorldsResponseParameterCode.WorldName, world.WorldName },
                        };
                        SendResponse(fetchCode, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("Fetch Worlds Invalid Cast!");
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
