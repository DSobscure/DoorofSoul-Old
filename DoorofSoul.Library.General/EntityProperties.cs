using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General
{
    public struct EntitySpaceProperties
    {
        public DSVector3 position;
        public DSVector3 rotation;
        public DSVector3 scale;

        public DSVector3 velocity;
        public DSVector3 maxVelocity;
        public DSVector3 angularVelocity;
        public DSVector3 maxAngularVelocity;
        public float mass;
    }
}
