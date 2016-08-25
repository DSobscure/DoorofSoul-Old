﻿using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class KnowledgeRepositoryList : InstantiableRepositoryList
    {
        public SkillRepository SkillRepository { get; protected set; }
        public StatusEffectRepository StatusEffectRepository { get; protected set; }
        public SkillsRepositoryList SkillsRepositoryList { get; protected set; }
        public StatusEffectsRepositoryList StatusEffectsRepositoryList { get; protected set; }
    }
}