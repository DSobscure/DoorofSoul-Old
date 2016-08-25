using DoorofSoul.Protocol;
using System;
using System.Collections.Generic;
using DoorofSoul.Library.General.ElementComponents;
using UnityEngine;

namespace DoorofSoul.Library.General.NatureComponents.SceneElements
{
    public class ItemEntityManager
    {
        protected Scene scene;
        protected Dictionary<int, ItemEntity> itemEntityDictionary;
        public IEnumerable<ItemEntity> ItemEntities { get { return itemEntityDictionary.Values; } }
        public float PickupRangeLimit { get; protected set; }

        private event Action<ItemEntity, DataChangeTypeCode> onItemEntityChange;
        public event Action<ItemEntity, DataChangeTypeCode> OnItemEntityChange { add { onItemEntityChange += value; } remove { onItemEntityChange -= value; } }

        public ItemEntityManager(Scene scene)
        {
            this.scene = scene;
            PickupRangeLimit = 50;
            itemEntityDictionary = new Dictionary<int, ItemEntity>();
        }
        public bool ContainsItemEntity(int itemEntityID)
        {
            return itemEntityDictionary.ContainsKey(itemEntityID);
        }
        public ItemEntity FindItemEntity(int itemEntityID)
        {
            if(ContainsItemEntity(itemEntityID))
            {
                return itemEntityDictionary[itemEntityID];
            }
            else
            {
                return null;
            }
        }
        public void LoadItemEntity(ItemEntity itemEntity)
        {
            if(itemEntity.LoacatedSceneID == scene.SceneID &&!ContainsItemEntity(itemEntity.ItemEntityID))
            {
                itemEntityDictionary.Add(itemEntity.ItemEntityID, itemEntity);
                onItemEntityChange?.Invoke(itemEntity, DataChangeTypeCode.Load);
            }
            else if(itemEntity.LoacatedSceneID == scene.SceneID)
            {
                itemEntityDictionary[itemEntity.ItemEntityID] = itemEntity;
                onItemEntityChange?.Invoke(itemEntity, DataChangeTypeCode.Update);
            }
        }
        public void UnloadItemEntity(ItemEntity itemEntity)
        {
            if (itemEntity.LoacatedSceneID == scene.SceneID && ContainsItemEntity(itemEntity.ItemEntityID))
            {
                itemEntityDictionary.Remove(itemEntity.ItemEntityID);
                onItemEntityChange?.Invoke(itemEntity, DataChangeTypeCode.Unload);
            }
        }
        public void InitialItemEntities(List<ItemEntity> itemEntities)
        {
            foreach(ItemEntity itemEntity in itemEntities)
            {
                if (itemEntity.LoacatedSceneID == scene.SceneID && !ContainsItemEntity(itemEntity.ItemEntityID))
                {
                    itemEntityDictionary.Add(itemEntity.ItemEntityID, itemEntity);
                }
            }
            onItemEntityChange?.Invoke(null, DataChangeTypeCode.Initial);
        }
        public void ClearAllItemEntities()
        {
            itemEntityDictionary.Clear();
            onItemEntityChange?.Invoke(null, DataChangeTypeCode.ClearAll);
        }
        public void CreateItemEntity(int itemID, DSVector3 position)
        {
            ItemEntity itemEntity = LibraryInstance.NatureInterface?.SceneElementsInterface.CreateItemEntity(itemID, scene.SceneID, position);
            if (itemEntity != null)
            {
                LoadItemEntity(itemEntity);
            }
        }
        public bool CanPickupItemEntity(Container container, int itemEntityID)
        {
            if(ContainsItemEntity(itemEntityID))
            {
                ItemEntity itemEntity = itemEntityDictionary[itemEntityID];
                if (((Vector3)container.Entity.Position - (Vector3)itemEntity.Position).magnitude <= PickupRangeLimit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public void DeleteItemEntity(int itemEntityID)
        {
            if (ContainsItemEntity(itemEntityID))
            {
                ItemEntity itemEntity = itemEntityDictionary[itemEntityID];
                UnloadItemEntity(itemEntity);
                LibraryInstance.NatureInterface?.SceneElementsInterface.DeleteItemEntity(itemEntityID);
            }
        }
    }
}
