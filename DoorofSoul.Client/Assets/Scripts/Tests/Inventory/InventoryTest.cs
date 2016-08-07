using UnityEngine;
using DoorofSoul.Client.Global;

public class InventoryTest : MonoBehaviour
{
    bool finished = false;
	
	// Update is called once per frame
	void Update ()
    {
	    if(!finished && Global.Seat.MainContainer.Inventory != null)
        {
            finished = true;
            var inventory = Global.Seat.MainContainer.Inventory;
            foreach(var info in inventory.ItemInfos)
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
