using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWindow : WindowObject
{
    [SerializeField] private List<Slot> slotList;
    [SerializeField] private List<Item> itemList;
    public void UpdateInventory()
    {
        int slotNum = slotList.Count;
        itemList = InventoryManager.Instance.GetItemList();
        int itemNum = itemList.Count;
        for (int i = 0; i < slotNum; i++)
        {
            if (i < itemNum)
            {
                slotList[i].SetImage(itemList[i].itemSprite);
            }
            else
            {
                slotList[i].SetImage(null);
            }
        }
    }
    public override void Activate()
    {
        UpdateInventory();
        ActivateObject();
        return;
    }
}
