using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Player;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Player.FetchData
{
    public class FetchSystemVersionHandler : FetchDataHandler
    {
        public FetchSystemVersionHandler(General.Player player) : base(player, 0)
        {
        }

        public override bool Handle(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    string serverVersion, clientVersion;
                    player.PlayerCommunicationInterface.GetSystemVersion(out serverVersion, out clientVersion);
                    var result = new Dictionary<byte, object>
                    {
                        { (byte)FetchSystemVersionResponseParameterCode.CurrentServerVersion, serverVersion },
                        { (byte)FetchSystemVersionResponseParameterCode.CurrentClientVersion, clientVersion }
                    };
                    SendResponse(fetchCode, result);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("Fetch System Version Invalid Cast!");
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
