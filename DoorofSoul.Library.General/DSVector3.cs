using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DoorofSoul.Library.General
{
    public struct DSVector3
    {
        public float x;
        public float y;
        public float z;

        public static explicit operator Vector3(DSVector3 vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }
    }
}
