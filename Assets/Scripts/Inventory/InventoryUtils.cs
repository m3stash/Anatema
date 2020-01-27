using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryUtils
{
    /// <summary>
    /// Get idx of empty slot of inventory
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public static int GetEmptySlotIdx(InventoryItemData[] items) {
        for(int i = 0; i < items.Length; i++) {
            if(!items[i]?.GetConfig()) {
                return i;
            }
        }

        return -1;
    }


    /// <summary>
    /// Get item idx of item found in function of parameters
    /// </summary>
    /// <param name="items"></param>
    /// <param name="item"></param>
    /// <param name="stackableFilter"></param>
    /// <returns></returns>
    public static int GetItemSlotIdx(InventoryItemData[] items, Item item, bool stackableFilter = false) {
        for(int i = 0; i < items.Length; i++) {
            if(items[i]?.GetConfig()) {
                bool itemFoundById = item.GetConfig().GetId().Equals(items[i].GetConfig().GetId());

                // Check if its same item ID and addition of stacks is less than stackLimit
                if(itemFoundById && ((stackableFilter && items[i].CanStack()) || !stackableFilter)) {
                    return i;
                }
            }
        }

        return -1;
    }

    /// <summary>
    /// Get item idx of item found in function of parameters
    /// </summary>
    /// <param name="items"></param>
    /// <param name="item"></param>
    /// <param name="stackableFilter"></param>
    /// <returns></returns>
    public static int GetItemSlotIdx(InventoryItemData[] items, InventoryItemData item, bool stackableFilter = false) {
        for(int i = 0; i < items.Length; i++) {
            if(items[i]?.GetConfig()) {
                bool itemFoundById = item.GetConfig().GetId().Equals(items[i].GetConfig().GetId());

                // Check if its same item ID and addition of stacks is less than stackLimit
                if(itemFoundById && ((stackableFilter && items[i].CanStack()) || !stackableFilter)) {
                    return i;
                }
            }
        }

        return -1;
    }
}
