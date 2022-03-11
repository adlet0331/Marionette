
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

    [SerializeField] private int selectedIdx = -1;
    [SerializeField] private int equipedIdx = -1;
    [SerializeField] private int selectionPannelInt = -1;

    private void updateInventoryUI()
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
                slotList[i].SetSlotStatus(i == selectedIdx, i == equipedIdx);
                slotList[i].SetImage(itemList[i].itemSprite);
            }
        }
    }

    public void OpenIteminfoUI()
    {
        itemInfoPannel.gameObject.SetActive(true);
        itemInfoPannel.OpenWindow(currentItemList[selectedIdx].itemSprite, currentItemList[selectedIdx].itemInfo);
        isInfoPannelOpen = true;
    }

    public void CloseItemInfoUI()
    {
        itemInfoPannel.gameObject.SetActive(false);
        isInfoPannelOpen = false;
    }

    public void OpenSelectionPannel()
    {
        selectionPannelInt = 0;
        isSelPannelOpen = true;
        InputManager.Instance.SetItemSelectionPannel(true);
        itemSelectionPannel.gameObject.SetActive(true);
        itemSelectionPannel.UpdatePannel(equipedIdx != selectedIdx);
    }

    public void CloseSelectionPannel()
    {
        isSelPannelOpen = false;
        itemSelectionPannel.gameObject.SetActive(false);
        InputManager.Instance.SetItemSelectionPannel(false);
    }

    public new void CloseWindow()
    {
        InputManager.Instance.SetOptions(true, true);
        InputManager.Instance.SetInventWind(false);
        InputManager.Instance.SetItemSelectionPannel(false);

        CloseItemInfoUI();
        CloseSelectionPannel();
        base.CloseWindow();
    }

    private void equipCurrentSelectedItem(bool isEquip)
    {
        if (isEquip)
            equipedIdx = selectedIdx;
        else
            equipedIdx = -1;
        itemSelectionPannel.UpdatePannel(!isEquip);
        updateInventoryUI();
        if (isEquip)
            WindowManager.Instance.profileWindow.UpdateEquipItem(1);
        else
            WindowManager.Instance.profileWindow.UpdateEquipItem(-1);
    }

    public void PressInteract()
    {
        if (!isSelPannelOpen)
        {
            OpenSelectionPannel();
        }
        else
        {
            if (selectionPannelInt == 0)
            {
                equipCurrentSelectedItem(selectedIdx != equipedIdx);
            }
            else if (selectionPannelInt == 1)
            {
                OpenIteminfoUI();
            }
            else if (selectionPannelInt == 2)
            {
                CloseSelectionPannel();
                CloseItemInfoUI();
            }
        }
    }

    public void MoveEquipWindowIdx(int moveInt)
    {
        selectionPannelInt += moveInt;
        if (selectionPannelInt < 0)
            selectionPannelInt += 3;
        else if (selectionPannelInt > 2)
            selectionPannelInt %= 3;

        itemSelectionPannel.UpdateUI(selectionPannelInt);
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
        selectedIdx += moveInt;

        if (currentItemList.Count == 0)
        {
            selectedIdx = -1;
            return;
        }
        else if (selectedIdx < 0)
        {
            if (moveInt == -1)
                selectedIdx = currentItemList.Count + selectedIdx;
            if (moveInt == -2)
                if (currentItemList.Count % 2 == 0)
                    selectedIdx = currentItemList.Count + selectedIdx;
                else
                    selectedIdx = currentItemList.Count - (3 + selectedIdx);
        }
        else if (selectedIdx >= currentItemList.Count)
            if (currentItemList.Count % 2 == 1 && moveInt == 2)
                if (selectedIdx == currentItemList.Count)
                    selectedIdx = 1;
                else
                    selectedIdx = 0;
            else
                selectedIdx = currentItemList.Count % currentItemList.Count;

        if (selectedIdx <= currentItemList.Count)
        {
            slotList[selectedIdx].SetSlotStatus(true, false);
        }

        updateInventoryUI();
        return;
    }

    public override void Activate()
    {
        if (itemSelectionPannel.gameObject.activeSelf)
        {
            CloseSelectionPannel();
            CloseItemInfoUI();
            return;
        }

        if (gameObject.activeSelf)
        {
            CloseWindow();
            return;
        }

        InputManager.Instance.SetOptions(false, true);
        InputManager.Instance.SetInventWind(true);
        UpdateInventory();
        OpenWindow();

        selectedIdx = 0;
        updateInventoryUI();

        return;
    }


}
