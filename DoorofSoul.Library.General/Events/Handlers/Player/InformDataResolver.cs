using DoorofSoul.Library.General.Events.Handlers.Player.InformData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Player
{
    public class InformDataResolver : PlayerEventHandler
    {
        protected readonly Dictionary<PlayerInformDataCode, InformDataHandler> informTable;

        public InformDataResolver(General.Player player) : base(player)
        {
            informTable = new Dictionary<PlayerInformDataCode, InformDataHandler>
            {
                { PlayerInformDataCode.FetchDataError, new InformFetchDataErrorHandler(player) },
                { PlayerInformDataCode.SystemVersion, new InformSystemVersionHandler(player) },
                { PlayerInformDataCode.Answer, new InformAnswerErrorHandler(player) },
            };
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Player Inform Data Event Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                PlayerInformDataCode informCode = (PlayerInformDataCode)parameters[(byte)InformDataEventParameterCode.InformCode];
                ErrorCode returnCode = (ErrorCode)parameters[(byte)InformDataEventParameterCode.ReturnCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)InformDataEventParameterCode.Parameters];
                if (informTable.ContainsKey(informCode))
                {
                    return informTable[informCode].Handle(informCode, returnCode, resolvedParameters);
                }
                else
                {
                    LibraryLog.ErrorFormat("Player InformData Event Not Exist Inform Code: {0}", informCode);
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
