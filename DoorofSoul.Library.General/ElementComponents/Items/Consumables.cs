using DoorofSoul.Library.General.LightComponents.Effects;
using DoorofSoul.Protocol;
using System.Collections.Generic;
using System.Linq;

namespace DoorofSoul.Library.General.ElementComponents.Items
{
    public class Consumables : ItemComponent
    {
        public int ConsumablesID { get; protected set; }

        public override ItemComponentTypeCode ItemComponentTypeCode { get { return ItemComponentTypeCode.Consumables; } }
        protected List<Effector> effectors;

        public Consumables(int consumablesID)
        {
            ConsumablesID = consumablesID;
            effectors = new List<Effector>();
        }
        public override bool Use(List<IEffectorTarget> targets)
        {
            if (effectors.Count != 0)
            {
                return effectors.All(x => x.Affect(targets));
            }
            else
            {
                return false;
            }
        }
        public void AddEffector(Effector effector)
        {
            effectors.Add(effector);
        }
        public void RemoveEffector(Effector effector)
        {
            effectors.Remove(effector);
        }
        public void LoadEffectors(IEnumerable<Effector> effectors)
        {
            this.effectors.AddRange(effectors);
        }
        public void ClearEffectors()
        {
            effectors.Clear();
        }
    }
}
