using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWindow : WindowObject
{
    [SerializeField] private List<Slot> slotList;
    [SerializeField] private List<Item> itemList;

    private void Start()
    {
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        int slotNum = slotList.Count;
        int itemNum = itemList.Count;
        int cnt = 0;
        for (int i = 0; i < slotNum; i++)
        {
            if (i < itemNum)
            {

            }
            else
            {
                slotList[i].SetImage(null);
            }
        }
    }
    public override void Activate()
    {
        return;
    }
}
