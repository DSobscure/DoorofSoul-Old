using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Answer;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.Responses.Handlers.Answer.FetchData
{
    public class FetchContainersResponseHandler : FetchDataResponseHandler
    {
        public FetchContainersResponseHandler(General.Answer answer) : base(answer)
        {
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 2)
                        {
                            LibraryLog.ErrorFormat(string.Format("Fetch Containers Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryLog.ErrorFormat("Fetch Containers Response Error DebugMessage: {0}", debugMessage);
                        answer.ErrorInform(LauguageDictionarySelector.Instance[answer.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[answer.UsingLanguage]["Fetch Container Error"]);
                        return false;
                    }
            }
        }

        public override bool Handle(AnswerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, returnCode, fetchDebugMessage, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)FetchContainersResponseParameterCode.ContainerID];
                    int entityID = (int)parameters[(byte)FetchContainersResponseParameterCode.EntityID];
                    General.Container container = new General.Container(containerID, entityID);
                    answer.LoadContainers(new List<General.Container> { container });
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.Error("Fetch Containers Response Parameter Cast Error");
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
