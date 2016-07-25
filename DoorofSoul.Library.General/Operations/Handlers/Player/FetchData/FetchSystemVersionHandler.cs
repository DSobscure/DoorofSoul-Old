﻿using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Player;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Player.FetchData
{
    public class FetchSystemVersionHandler : FetchDataHandler
    {
        public FetchSystemVersionHandler(General.Player player) : base(player)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = string.Format("Player Fetch System Version Parameter Error Parameter Count: {0}", parameter.Count);
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
                    string serverVersion, clientVersion;
                    player.FetchSystemVersion(out serverVersion, out clientVersion);
                    var result = new Dictionary<byte, object>
                    {
                        { (byte)InformSystemVersionParameterCode.CurrentServerVersion, serverVersion },
                        { (byte)InformSystemVersionParameterCode.CurrentClientVersion, clientVersion }
                    };
                    SendEvent(PlayerInformDataCode.SystemVersion, result);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("Fetch System Version Invalid Cast!");
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