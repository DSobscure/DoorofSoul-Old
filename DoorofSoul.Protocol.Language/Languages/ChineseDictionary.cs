using System.Collections.Generic;

namespace DoorofSoul.Protocol.Language.Languages
{
    public class ChineseDictionary : LauguageDictionary
    {
        public ChineseDictionary() : base()
        {
            dictionary = new Dictionary<string, string>
            {
                #region ErrorInform Title
                { "Permission Deny", "權限不足" },
                { "Fail", "失敗" },
                { "Unknown Error", "未知的錯誤" },
                { "Invalid Operation", "非法指令" },
                #endregion
                #region ErroInform Content
                { "Activate Soul Fail", "連接靈魂失敗" },
                { "Logout Fail", "登出失敗" },
                { "Login Fail", "帳號登入中" },
                { "Login InvalidOperation", "帳號密碼錯誤" },
                { "Login Error", "登入錯誤" },
                #endregion


                { "Operation Parameter Error", "操作參數錯誤"},
                { "Fetch Operation Parameter Error", "擷取操作參數錯誤"},
                { "Not Existed Fetch Operation", "不存在的擷取操作"},
                { "Account or Password Error", "帳號或密碼錯誤" },
                { "Logout Failed", "登出失敗" },
                
                { "Delete Soul Error", "刪除靈魂錯誤" },
                { "Activate Soul Error", "連接靈魂錯誤" },
                { "Create Soul Error", "分離靈魂錯誤" },
                { "Already Login", "此帳號登入中" },
                { "Client Version Inconsistent", "客戶端版本無法執行最新的遊戲內容\n請下載最新的版本" },
                { "SoulCountLimit", "靈魂數量上限" },
            };
        }
    }
}
