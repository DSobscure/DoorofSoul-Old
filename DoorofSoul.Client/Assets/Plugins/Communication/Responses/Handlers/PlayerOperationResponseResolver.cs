﻿using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace DoorofSoul.Client.Communication.Responses.Handlers
{
    public class PlayerOperationResponseResolver : ResponseHandler
    {
        public PlayerOperationResponseResolver(PhotonService photonService) : base(photonService)
        {
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch(returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 5)
                        {
                            debugMessage = string.Format("Player Operation Parameter Error Parameter Count: {0}", parameters.Count);
                            return false;
                        }
                        else
                        {
                            debugMessage = null;
                            return true;
                        }
                    }
                default:
                    {
                        photonService.DebugReturn(DebugLevel.ERROR, string.Format("Player Operation Unknown Error DebugMessage: {0}", debugMessage));
                        return false;
                    }
            }
        }

        public override bool Handle(OperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                try
                {
                    int playerID = (int)parameters[(byte)ResponseParameterCode.ID];
                    PlayerOperationCode resolvedOperationCode = (PlayerOperationCode)parameters[(byte)ResponseParameterCode.OperationCode];
                    ErrorCode resolvedReturnCode = (ErrorCode)parameters[(byte)ResponseParameterCode.ReturnCode];
                    string resolvedDebugMessage = (string)parameters[(byte)ResponseParameterCode.DebugMessage];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)ResponseParameterCode.Parameters];
                    if (playerID == Global.Global.Player.PlayerID || Global.Global.Player.PlayerID == 0)
                    {
                        Global.Global.Player.PlayerResponseManager.Operate(resolvedOperationCode, resolvedReturnCode, resolvedDebugMessage, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        photonService.DebugReturn(DebugLevel.ERROR, string.Format("PlayerResponse Error PlayerID: {0} Not In Peer", playerID));
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    photonService.DebugReturn(DebugLevel.ERROR, "PlayerResponse Parameter Cast Error");
                    photonService.DebugReturn(DebugLevel.ERROR, ex.Message);
                    photonService.DebugReturn(DebugLevel.ERROR, ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    photonService.DebugReturn(DebugLevel.ERROR, ex.Message);
                    photonService.DebugReturn(DebugLevel.ERROR, ex.StackTrace);
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