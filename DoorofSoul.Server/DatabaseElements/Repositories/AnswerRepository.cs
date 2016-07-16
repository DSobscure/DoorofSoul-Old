﻿using DoorofSoul.Library.General;

namespace DoorofSoul.Server.DatabaseElements.Repositories
{
    public abstract class AnswerRepository
    {
        public abstract Answer Find(int answerID);
        public abstract void Save(Answer answer);
    }
}
