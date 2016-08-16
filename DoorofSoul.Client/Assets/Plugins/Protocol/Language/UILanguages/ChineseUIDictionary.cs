using System.Collections.Generic;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Client.Protocol.Language.UILanguages
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
                { "Inventory", "物品欄" },
                
                #endregion

                #region skill panel
                { "Alchemy", "構成系統" },
                { "Element", "元素系統" },
                { "Genie", "精靈系統" },
                { "Demon", "魔幻系統" },
                { "Chance", "奇跡系統" },
                { "Technology", "科技系統" },
                { "Belief", "信念系統" },

                { "List", "清單" },
                { "HeptagramSystem", "系統" },
                { "ViewMode", "檢視方法" },

                { "C", "C" },
                { "C_sharp", "C♯" },
                { "D_flat", "D♭" },
                { "D", "D" },
                { "D_sharp", "D♯" },
                { "E_flat", "E♭" },
                { "E", "E" },
                { "F", "F" },
                { "F_sharp", "F♯" },
                { "G_flat", "G♭" },
                { "G", "G" },
                { "G_sharp", "G♯" },
                { "A_flat", "A♭" },
                { "A", "A" },
                { "A_sharp", "A♯" },
                { "B_flat", "B♭" },
                { "B", "B" },

                { "BasicLifePointCost", "基礎生命消耗" },
                { "BasicEnergyPointCost", "基礎能量消耗" },
                { "BasicCorePointCost", "基礎魂魄消耗" },
                { "BasicSpiritPointCost", "基礎精神消耗" },
                #endregion
            };
        }
    }
}
