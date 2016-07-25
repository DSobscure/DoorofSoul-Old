using System;
using DoorofSoul.Library.General;

namespace DoorofSoul.Database.Library
{
    public class DatabaseAnswer : Answer
    {
        public DatabaseAnswer(int answerID, int soulCountLimit, Player player) : base(answerID, soulCountLimit, player)
        {
        }

        public override bool ActiveSoul(int soulID)
        {
            throw new NotImplementedException();
        }

        public override bool CreateSoul(string soulName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteSoul(int soulID)
        {
            throw new NotImplementedException();
        }
    }
}
