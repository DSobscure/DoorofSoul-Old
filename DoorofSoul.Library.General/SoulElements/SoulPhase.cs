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
        public int UnderstandingPoint { get; protected set; }
        public int MaxUnderstandingPoint { get; protected set; }

        public SoulPhase() { }
        public SoulPhase(byte phaseLevel, byte understandingLevel, int understandingPoint)
        {
            PhaseLevel = phaseLevel;
            UnderstandingLevel = understandingLevel;
            UnderstandingPoint = understandingPoint;
        }
    }
}
