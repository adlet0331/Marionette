
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryWindow : WindowObject
{
    [SerializeField] private List<Slot> slotList;
    [SerializeField] private List<ItemData> itemList;
    [SerializeField] private int selectedInt;
    [SerializeField] private int equipedInt;

    private void updateSelectUI()
    {
        List<ItemData> itemList = InventoryManager.Instance.GetItemList();
        for (int i=0; i<10; i++)
        {
            if (i >= itemList.Count)
            {
                slotList[i].SetSlotStatus(i == selectedInt, false);
                slotList[i].SetImage(null);
            }
            else
            {
                slotList[i].SetSlotStatus(i == selectedInt, i == equipedInt);
                slotList[i].SetImage(itemList[i].itemSprite);
            }
        }
    }

    public new void CloseWindow()
    {
        InputManager.Instance.SetOptions(true, true);
        InputManager.Instance.SetInventWind(false);
        base.CloseWindow();
    }

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

    public void UpdateSelectSlot(int moveInt)
    {
        selectedInt += moveInt;

        if (selectedInt < 0)
            selectedInt = 0;
        else if (selectedInt > 9)
            selectedInt = 9;

        updateSelectUI();
        return;
    }

    public override void Activate()
    {
        InputManager.Instance.SetOptions(false, true);
        InputManager.Instance.SetInventWind(true);
        UpdateInventory();
        ActivateObject();

        selectedInt = 0;
        updateSelectUI();

        return;
    }


}
