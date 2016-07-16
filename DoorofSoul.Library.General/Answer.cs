using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General
{
    public class Answer
    {
        public int AnswerID { get; protected set; }
        protected Dictionary<int, Soul> soulDictionary;
        public IEnumerable<Soul> Souls { get { return soulDictionary.Values; } }
        protected Dictionary<int, Container> containerDictionary;
        public IEnumerable<Container> Containers { get { return containerDictionary.Values; } }

        public Answer(int answerID)
        {
            AnswerID = answerID;
            soulDictionary = new Dictionary<int, Soul>();
            containerDictionary = new Dictionary<int, Container>();
        }
    }
}
