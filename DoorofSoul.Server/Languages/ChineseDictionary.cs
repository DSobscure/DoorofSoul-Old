using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            };
        }
    }
}
