﻿using System.Collections.Generic;

namespace DoorofSoul.Server.Languages
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
                { "Account or Password Error", "帳號或密碼錯誤" },
                { "Logout Failed", "登出失敗" },
                { "Permission Deny", "權限不足" },
                { "Delete Soul Error", "刪除靈魂錯誤" },
                { "Activate Soul Error", "照射靈魂錯誤" },
                { "Create Soul Error", "分離靈魂錯誤" }
            };
        }
    }
}
