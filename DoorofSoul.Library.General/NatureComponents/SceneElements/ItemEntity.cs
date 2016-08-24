using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Library.General.ElementComponents;

namespace DoorofSoul.Library.General.NatureComponents.SceneElements
{
    public class ItemEntity
    {
        public int ItemEntityID { get; protected set; }
        public int ItemID { get; protected set; }
        public int LoacatedSceneID { get; protected set; }
        public DSVector3 Position { get; protected set; }

        public ItemEntity(int itemEntityID, int itemID, int loacatedSceneID, DSVector3 position)
        {
            ItemEntityID = itemEntityID;
            ItemID = itemID;
            LoacatedSceneID = loacatedSceneID;
            Position = position;
        }
    }
}
