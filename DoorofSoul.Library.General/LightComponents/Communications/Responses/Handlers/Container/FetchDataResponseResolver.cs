﻿using DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Container.FetchData;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Container
{
    internal class FetchDataResponseResolver : ContainerResponseHandler
    {
        protected readonly Dictionary<ContainerFetchDataCode, FetchDataResponseHandler> fetchResponseTable;

        internal FetchDataResponseResolver(NatureComponents.Container container) : base(container)
        {
            fetchResponseTable = new Dictionary<ContainerFetchDataCode, FetchDataResponseHandler>
            {
                { ContainerFetchDataCode.Entity, new FetchEntityResponseHandler(container) },
                { ContainerFetchDataCode.Inventory, new FetchInventoryResponseHandler(container) },
                { ContainerFetchDataCode.InventoryItems, new FetchInventoryItemsResponseHandler(container) },
                { ContainerFetchDataCode.ContainerStatusEffectInfos, new FetchContainerStatusEffectInfosResponseHandler(container) },
            };
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            if (returnCode == ErrorCode.NoError)
            {
                if (parameters.Count != 4)
                {
                    LibraryInstance.ErrorFormat("Container Fetch Data Response Parameter Error Parameter Count: {0}", parameters.Count);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("ContainerOperationResponse Error ErrorCode: {0}, DebugMessage: {1}", returnCode, debugMessage);
                return false;
            }
        }

        internal override bool Handle(ContainerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                ContainerFetchDataCode fetchCode = (ContainerFetchDataCode)parameters[(byte)FetchDataResponseParameterCode.FetchCode];
                ErrorCode resolvedReturnCode = (ErrorCode)parameters[(byte)FetchDataResponseParameterCode.ReturnCode];
                string resolvedDebugMessage = (string)parameters[(byte)FetchDataResponseParameterCode.DebugMessage];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataResponseParameterCode.Parameters];
                if (fetchResponseTable.ContainsKey(fetchCode))
                {
                    return fetchResponseTable[fetchCode].Handle(fetchCode, resolvedReturnCode, resolvedDebugMessage, resolvedParameters);
                }
                else
                {
                    LibraryInstance.ErrorFormat("Container FetchData Response Not Exist Fetch Code: {0}", fetchCode);
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