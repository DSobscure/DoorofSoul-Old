using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General.KnowledgeComponents
{
    public class StatusEffect
    {
        public int StatusEffectID { get; protected set; }
        public string StatusEffectName { get; protected set; }
        public float StandardEffectDuration { get; protected set; }

        public StatusEffect() { }
        public StatusEffect(int statusEffectID, string statusEffectName, float standardEffectDuration)
        {
            StatusEffectID = statusEffectID;
            StatusEffectName = statusEffectName;
            StandardEffectDuration = standardEffectDuration;
        }
    }
}
