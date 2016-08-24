using UnityEngine;

namespace DoorofSoul.Client.Scripts.HistoryScripts.TestScripts.InventoryTestScripts
{
    public class InventoryTest : MonoBehaviour
    {
        bool finished = false;

        void Update()
        {
            if (!finished && Global.Global.Seat.MainContainer.Inventory != null)
            {
                finished = true;
                var inventory = Global.Global.Seat.MainContainer.Inventory;
                foreach (var info in inventory.ItemInfos)
                {
                    Debug.Log(info.item.ItemID);
                    Debug.Log(info.item.ItemName);
                    Debug.Log(info.item.Description);
                    Debug.Log(info.count);
                    Debug.Log(info.positionIndex);
                }
            }
        }
    }
}
