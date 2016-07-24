using DoorofSoul.Protocol.Language.Languages;
using System.Collections.Generic;

namespace DoorofSoul.Protocol.Language
{
    public class LauguageDictionarySelector
    {
        private static LauguageDictionarySelector instance;
        public static LauguageDictionarySelector Instance { get { return instance; } }
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

        static LauguageDictionarySelector()
        {
            if (instance == null)
                instance = new LauguageDictionarySelector();
        }
        protected LauguageDictionarySelector()
        {
            lauguageDictionarys = new Dictionary<SupportLauguages, LauguageDictionary>
            {
                { SupportLauguages.Chinese_Traditional, new ChineseDictionary() }
            };
        }
    }
}
