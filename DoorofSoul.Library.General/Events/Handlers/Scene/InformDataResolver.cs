using DoorofSoul.Library.General.Events.Handlers.Scene.InformData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Scene
{
    public class InformDataResolver : SceneEventHandler
    {
        protected readonly Dictionary<SceneInformDataCode, InformDataHandler> informTable;

        public InformDataResolver(General.Scene scene) : base(scene)
        {
            informTable = new Dictionary<SceneInformDataCode, InformDataHandler>
            {
                { SceneInformDataCode.FetchDataError, new InformFetchDataErrorHandler(scene) },
                { SceneInformDataCode.Entity, new InformEntityHandler(scene) },
                { SceneInformDataCode.EntityEnter, new InformEntityEnterHandler(scene) },
                { SceneInformDataCode.EntityExit, new InformEntityExitHandler(scene) },
            };
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Scene Inform Data Event Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                SceneInformDataCode informCode = (SceneInformDataCode)parameters[(byte)InformDataEventParameterCode.InformCode];
                ErrorCode returnCode = (ErrorCode)parameters[(byte)InformDataEventParameterCode.ReturnCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)InformDataEventParameterCode.Parameters];
                if (informTable.ContainsKey(informCode))
                {
                    return informTable[informCode].Handle(informCode, returnCode, resolvedParameters);
                }
                else
                {
                    LibraryLog.ErrorFormat("Scene InformData Event Not Exist Inform Code: {0}", informCode);
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
