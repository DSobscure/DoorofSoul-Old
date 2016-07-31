using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Language.UILanguages;

namespace DoorofSoul.Protocol.Language
{
    public class UILanguageSeletor
    {
        private static UILanguageSeletor instance;
        public static UILanguageSeletor Instance { get { return instance; } }
        private Dictionary<SupportLauguages, LauguageDictionary> lauguageDictionarys;

        public LauguageDictionary this[SupportLauguages key]
        {
            get
            {
                if (lauguageDictionarys.ContainsKey(key))
                {
                    return lauguageDictionarys[key];
                }
                else
                {
                    return null;
                }
            }
        }

        static UILanguageSeletor()
        {
            if (instance == null)
                instance = new UILanguageSeletor();
        }
        protected UILanguageSeletor()
        {
            lauguageDictionarys = new Dictionary<SupportLauguages, LauguageDictionary>
            {
                { SupportLauguages.Chinese_Traditional, new ChineseUIDictionary() }
            };
        }
    }
}
