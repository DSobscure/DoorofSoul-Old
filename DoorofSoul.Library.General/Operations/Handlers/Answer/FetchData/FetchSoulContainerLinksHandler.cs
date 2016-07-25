﻿using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Answer;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Answer.FetchData
{
    public class FetchSoulContainerLinksHandler : FetchDataHandler
    {
        public FetchSoulContainerLinksHandler(General.Answer answer) : base(answer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = string.Format("Answer Fetch SoulContainerLinks Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(AnswerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach (General.Soul soul in answer.Souls)
                    {
                        foreach (General.Container container in soul.Containers)
                        {
                            var result = new Dictionary<byte, object>
                            {
                                { (byte)InformSoulContainerLinkParameterCode.SoulID, soul.SoulID },
                                { (byte)InformSoulContainerLinkParameterCode.ContainerID, container.ContainerID }
                            };
                            SendEvent(AnswerInformDataCode.SoulContainerLink, result);
                        }
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("Fetch Soul Container Links Invalid Cast!");
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