using DoorofSoul.Protocol.Communication.InformDataCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.World
{
    public abstract class InformDataHandler
    {
        protected NatureComponents.World world;
        protected int correctParameterCount;

        protected InformDataHandler(NatureComponents.World world, int correctParameterCount)
        {
            this.world = world;
            this.correctParameterCount = correctParameterCount;
        }

        public virtual bool Handle(WorldInformDataCode informCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                LibraryInstance.ErrorFormat("World InformData Parameter Error On {0} WorldID: {1} Debug Message: {2}", informCode, world.WorldID, debugMessage);
                return false;
            }
        }
        internal virtual bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != correctParameterCount)
            {
                debugMessage = string.Format("Parameter Count: {0} Should be {1}", parameters.Count, correctParameterCount);
                return false;
            }
            else
            {
                debugMessage = "";
                return true;
            }
        }
    }
}
