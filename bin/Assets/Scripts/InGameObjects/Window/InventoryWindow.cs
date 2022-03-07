
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryWindow : WindowObject
{
    [SerializeField] private ItemSelectionPannel itemSelectionPannel;
    [SerializeField] private ItemInfoPannel itemInfoPannel;

    [SerializeField] private List<Slot> slotList;
    [SerializeField] private List<ItemData> currentItemList;
    [SerializeField] private int selectedInt = -1;
    [SerializeField] private int equipedInt = -1;

    private void updateSelectUI()
    {
        List<ItemData> itemList = InventoryManager.Instance.GetItemList();
        for (int i=0; i < 10; i++)
        {
            if (i >= itemList.Count)
            {
                slotList[i].SetSlotStatus(false, false);
                slotList[i].SetImage(null);
            }
            else
            {
                slotList[i].SetSlotStatus(i == selectedInt, i == equipedInt);
                slotList[i].SetImage(itemList[i].itemSprite);
            }
        }
    }

    private void openIteminfoUI()
    {
        itemInfoPannel.gameObject.SetActive(true);
        itemInfoPannel.OpenWindow(currentItemList[selectedInt].itemSprite, currentItemList[selectedInt].itemInfo);
    }

    private void closeItemInfoUI()
    {
        itemInfoPannel.gameObject.SetActive(false);
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
        currentItemList = InventoryManager.Instance.GetItemList();
        int itemNum = currentItemList.Count;
        for (int i = 0; i < slotNum; i++)
        {
            if (i < itemNum)
            {
                slotList[i].SetImage(currentItemList[i].itemSprite);
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

        if (currentItemList.Count == 0)
        {
            selectedInt = -1;
            return;
        }
        else if (selectedInt < 0)
        {
            if (moveInt == -1)
                selectedInt = currentItemList.Count + selectedInt;
            if (moveInt == -2)
                if (currentItemList.Count % 2 == 0)
                    selectedInt = currentItemList.Count + selectedInt;
                else
                    selectedInt = currentItemList.Count - (3 + selectedInt);
        }
        else if (selectedInt >= currentItemList.Count)
            if (currentItemList.Count % 2 == 0)
                selectedInt = currentItemList.Count + selectedInt;
            else
                selectedInt = currentItemList.Count - (3 + selectedInt); //asdfasdfasdf

        if (selectedInt <= currentItemList.Count)
        {
            slotList[selectedInt].SetSlotStatus(true, false);
            openIteminfoUI();
        }
        else
        {
            closeItemInfoUI();
        }

        updateSelectUI();
        return;
    }

    public override void Activate()
    {
        if (gameObject.activeSelf)
        {
            CloseWindow();
            return;
        }

        InputManager.Instance.SetOptions(false, true);
        InputManager.Instance.SetInventWind(true);
        UpdateInventory();
        OpenWindow();

        selectedInt = 0;
        updateSelectUI();

        return;
    }


}
