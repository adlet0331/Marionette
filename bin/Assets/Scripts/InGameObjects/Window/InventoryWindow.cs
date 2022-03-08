
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryWindow : WindowObject
{
    [SerializeField] public bool isSelPannelOpen;
    [SerializeField] public bool isInfoPannelOpen;

    [SerializeField] private ItemSelectionPannel itemSelectionPannel;
    [SerializeField] private ItemInfoPannel itemInfoPannel;

    [SerializeField] private List<Slot> slotList;
    [SerializeField] private List<ItemData> currentItemList;
    [SerializeField] private int selectedInt = -1;
    [SerializeField] private int equipedInt = -1;
    [SerializeField] private int selectionPannelInt = -1;

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

    private void updateSelectionUI()
    {

    }

    public void OpenIteminfoUI()
    {
        itemInfoPannel.gameObject.SetActive(true);
        itemInfoPannel.OpenWindow(currentItemList[selectedInt].itemSprite, currentItemList[selectedInt].itemInfo);
        isInfoPannelOpen = true;
    }

    public void CloseItemInfoUI()
    {
        itemInfoPannel.gameObject.SetActive(false);
        isInfoPannelOpen = false;
    }

    public void OpenSelectionPannel()
    {
        itemSelectionPannel.gameObject.SetActive(true);
        isSelPannelOpen = true;
    }

    public void CloseSelectionPannel()
    {
        itemSelectionPannel.gameObject.SetActive(false);
        isSelPannelOpen = false;
    }

    public new void CloseWindow()
    {
        InputManager.Instance.SetOptions(true, true);
        InputManager.Instance.SetInventWind(false);
        base.CloseWindow();
    }

    public void PressInteract()
    {
        if (!itemSelectionPannel.gameObject.activeSelf)
        {
            selectionPannelInt = 0;
            itemSelectionPannel.gameObject.SetActive(true);
            isSelPannelOpen = true;
        }
        else
        {
            itemSelectionPannel.Interact(selectionPannelInt);
        }
    }

    public void MoveEquipWindowIdx(int moveInt)
    {
        selectionPannelInt += moveInt;
        if (selectionPannelInt < 0)
            selectionPannelInt += 3;
        else if (selectionPannelInt > 2)
            selectionPannelInt %= 3;


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

    public void MoveInventoryUIdx(int moveInt)
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
            if (currentItemList.Count % 2 == 1 && moveInt == 2)
                if (selectedInt == currentItemList.Count)
                    selectedInt = 1;
                else
                    selectedInt = 0;
            else
                selectedInt = currentItemList.Count % currentItemList.Count;

        if (selectedInt <= currentItemList.Count)
        {
            slotList[selectedInt].SetSlotStatus(true, false);
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
