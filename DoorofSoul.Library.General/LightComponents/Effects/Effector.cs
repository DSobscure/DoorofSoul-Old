using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Effects
{
    public abstract class Effector
    {
        public abstract bool Affect(List<IEffectorTarget> targets);
    }
}
