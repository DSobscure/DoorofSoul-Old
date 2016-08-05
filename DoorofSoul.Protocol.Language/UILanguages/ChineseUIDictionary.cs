using System.Collections.Generic;

namespace DoorofSoul.Protocol.Language.UILanguages
{
    public class ChineseUIDictionary : LauguageDictionary
    {
        public ChineseUIDictionary() : base()
        {
            dictionary = new Dictionary<string, string>
            {
                #region player panel
                { "Unfold", "展開" },
                { "Fold", "摺疊" },
                { "Hold", "舉牌" },
                { "Send", "送出" },
                { "Source", "來源" },
                { "Target", "對象" },
                #endregion
            };
        }
    }
}
