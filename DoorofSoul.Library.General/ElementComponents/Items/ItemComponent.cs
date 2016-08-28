using DoorofSoul.Library.General.LightComponents.Effects;
using DoorofSoul.Protocol;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.ElementComponents.Items
{
    public abstract class ItemComponent
    {
        public abstract ItemComponentTypeCode ItemComponentTypeCode { get;}
        public abstract bool Use(List<IEffectorTarget> targets); 
    }
}
