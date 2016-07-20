using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationParameters;
using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Managers.OperationManagers
{
    public class OperationManager
    {
        public static void SendOperation(OperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Global.PhotonService.SendOperation(operationCode, parameters);
        }

        public void PlayerLogin(string account, string password)
        {
            var parameters = new Dictionary<byte, object>
            {
                { (byte)PlayerLoginOperationParameterCode.Account, account },
                { (byte)PlayerLoginOperationParameterCode.Password, password }
            };
            SendOperation(OperationCode.PlayerLogin, parameters);
        }
        public void PlayerLogout()
        {
            var parameters = new Dictionary<byte, object>();
            SendOperation(OperationCode.PlayerLogout, parameters);
        }
        public void CreateSoul(string soulName)
        {
            var parameters = new Dictionary<byte, object>
            {
                { (byte)CreateSoulOperationParameterCode.SoulName, soulName },
            };
            SendOperation(OperationCode.CreateSoul, parameters);
        }
        public void DeleteSoul(int soulID)
        {
            var parameters = new Dictionary<byte, object>
            {
                { (byte)DeleteSoulOperationParameterCode.SoulID, soulID },
            };
            SendOperation(OperationCode.DeleteSoul, parameters);
        }
        public void ActivateSoul(int soulID)
        {
            var parameters = new Dictionary<byte, object>
            {
                { (byte)ActivateSoulOperationParameterCode.SoulID, soulID },
            };
            SendOperation(OperationCode.ActivateSoul, parameters);
        }
    }
}
