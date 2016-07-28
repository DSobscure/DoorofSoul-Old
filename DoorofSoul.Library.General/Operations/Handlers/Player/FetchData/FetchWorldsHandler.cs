using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Player;

namespace DoorofSoul.Library.General.Operations.Handlers.Player.FetchData
{
    public class FetchWorldsHandler : FetchDataHandler
    {
        public FetchWorldsHandler(General.Player player) : base(player)
        {
            
        }

        public override bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 0)
            {
                debugMessage = string.Format("Player Fetch Worlds Parameter Error Parameter Count: {0}", parameters.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    List<General.World> worlds;
                    player.FetchWorlds(out worlds);
                    foreach(General.World world in worlds)
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
                    LibraryLog.ErrorFormat("Fetch Worlds Invalid Cast!");
                    LibraryLog.Error(ex.Message);
                    LibraryLog.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryLog.Error(ex.Message);
                    LibraryLog.Error(ex.StackTrace);
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
