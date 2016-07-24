using System.Collections.Generic;

namespace DoorofSoul.Protocol.Language
{
    public abstract class LauguageDictionary
    {
        protected Dictionary<string, string> dictionary;
        public string this[string key]
        {
            get
            {
                if (dictionary.ContainsKey(key))
                {
                    return dictionary[key];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
