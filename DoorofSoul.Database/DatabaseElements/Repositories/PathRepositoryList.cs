using System;
using DoorofSoul.Database.DatabaseElements.Repositories.Path;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class PathRepositoryList
    {
        public abstract Seperation_ConcretionRepositoryList Seperation_ConcretionRepositoryList { get; }
        public abstract Ego_CognitionRepositoryList Ego_CognitionRepositoryList { get; }
    }
}
