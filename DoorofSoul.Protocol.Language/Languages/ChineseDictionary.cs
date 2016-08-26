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
                { "Not Exist", "不存在" },
                #endregion
                #region ErroInform Content
                { "Activate Soul Fail", "連接靈魂失敗" },
                { "Logout Fail", "登出失敗" },
                { "Login Fail", "帳號登入中" },
                { "Login InvalidOperation", "帳號密碼錯誤" },
                { "Login Error", "登入錯誤" },
                { "Fetch SystemVersion Error", "讀取版本錯誤" },
                { "Fetch Entities Error", "讀取場景實體錯誤" },
                { "Fetch Answer Error", "讀取解答錯誤" },
                { "Fetch Soul Error", "讀取靈魂錯誤" },
                { "Fetch SoulContainerLinks Error", "讀取[靈魂 容器]連結錯誤" },
                { "Create Soul Fail", "分離靈魂失敗" },
                { "Create Soul PermissionDeny", "無法分離靈魂" },
                { "Create Soul Error", "分離靈魂錯誤" },
                { "Delete Soul Fail", "刪除靈魂失敗" },
                { "Delete Soul PermissionDeny", "無法刪除靈魂" },
                { "Delete Soul Error", "刪除靈魂錯誤" },
                { "Fetch Scene NotExist", "讀取了不存在的場景" },
                { "Fetch Scene Error", "讀取場景錯誤" },
                { "Fetch Container Error", "讀取容器錯誤" },
                { "Fetch Worlds Error", "讀取世界錯誤" },
                { "Fetch Entity Error", "讀取實體錯誤" },
                { "Fetch SkillInfos Error", "讀取技能資訊錯誤" },
                { "Fetch InventoryItems Error", "讀取道具欄內容資訊錯誤" },
                { "Fetch ContainerStatusEffectInfos Error", "讀取容器狀態資訊錯誤" },
                { "Fetch ItemEntities Error", "讀取場景物品實體錯誤" },
                { "Register Error", "註冊錯誤" },
                { "Account Used", "此帳號已經被使用了" },
                { "AccountOrPasswordInvalid", "帳號或密碼不合法" },
                #endregion

                #region UI
                { "SoulCountLimit", "靈魂數量上限" },
                #endregion

                { "Operation Parameter Error", "操作參數錯誤"},
                { "Fetch Operation Parameter Error", "擷取操作參數錯誤"},
                { "Not Existed Fetch Operation", "不存在的擷取操作"},
                
                { "Client Version Inconsistent", "客戶端版本無法執行最新的遊戲內容\n請下載最新的版本" },
                
            };
        }
    }
}
