using System;
using DoorofSoul.Library.General;
using DoorofSoul.Database.Library;

namespace DoorofSoul.Library
{
    public class HexagramAnswer : Answer
    {
        public HexagramAnswer(DatabaseAnswer answer) : base(answer.AnswerID, answer.SoulCountLimit, answer.Player)
        {
        }

        public override bool ActiveSoul(int soulID)
        {
            return Hexagram.Instance.Throne.ActiveSoul(soulID);
        }

        public override bool CreateSoul(string soulName)
        {
            return Hexagram.Instance.Throne.CreateSoul(AnswerID, soulName);
        }

        public override bool DeleteSoul(int soulID)
        {
            return Hexagram.Instance.Throne.DeleteSoul(AnswerID, soulID);
        }
    }
}
