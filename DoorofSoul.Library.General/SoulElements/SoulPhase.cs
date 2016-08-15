using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General.SoulElements
{
    public class SoulPhase
    {
        public byte PhaseLevel { get; protected set; }
        public byte UnderstandingLevel { get; protected set; }

        private int understandingPoint;
        public int UnderstandingPoint { get { return understandingPoint; } protected set { understandingPoint = Math.Max(Math.Min(value, MaxUnderstandingPoint), 0); onUnderstandingPointChange?.Invoke(understandingPoint); } }
        public int MaxUnderstandingPoint { get; protected set; } = int.MaxValue;

        private event Action<int> onUnderstandingPointChange;
        public event Action<int> OnUnderstandingPointChange { add { onUnderstandingPointChange += value; } remove { onUnderstandingPointChange -= value; } }

        public SoulPhase() { }
        public SoulPhase(byte phaseLevel, byte understandingLevel, int understandingPoint)
        {
            PhaseLevel = phaseLevel;
            UnderstandingLevel = understandingLevel;
            UnderstandingPoint = understandingPoint;
        }
    }
}
