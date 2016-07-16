using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General
{
    public class Soul
    {
        public int SoulID { get; protected set; }
        public int AnswerID { get; protected set; }
        protected Answer answer;
        public string SoulName { get; set; }
        protected Dictionary<int, Container> containerDictionary;

        public Soul(int soulID, int answerID, string soulName)
        {
            SoulID = soulID;
            AnswerID = answerID;
            SoulName = soulName;
            containerDictionary = new Dictionary<int, Container>();
        }
    }
}
