using System.Collections.Generic;

namespace DoorofSoul.Client.Communication.Languages
{
    public class ChineseDictionary : LauguageDictionary
    {
        public ChineseDictionary() : base()
        {
            dictionary = new Dictionary<string, string>
            {
                { "Operation Parameter Error", "操作參數錯誤"},
                { "Not Existed Fetch Operation", "不存在的擷取操作"},
                { "Fetch Operation Parameter Error", "擷取操作參數錯誤"},
                { "Client Version Inconsistent", "客戶端版本無法執行最新的遊戲內容\n請下載最新的版本" },
                { "SoulCountLimit", "靈魂數量上限" },
            };
        }
    }
}
