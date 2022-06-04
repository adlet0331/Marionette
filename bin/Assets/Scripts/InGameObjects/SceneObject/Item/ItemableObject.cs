using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Item 지급 / 삭제 가능한 오브젝트
 * 
 */
public enum ItemableObjectType
{
    AddItem = 0,
    DeleteItem = 1,
}
public class ItemableObject : InteractionObject
{
    [SerializeField] private ItemableObjectType itemType;
    [SerializeField] private bool deleteAfterInteract;
    [SerializeField] private List<int> itemIndexList;
    private void Start()
    {
        this.objectType = InteractionObjectType.ItemableObject;
        this.deleteAfterInteract = true;
    }
    public override void Interact()
    {
        var itemDataList = DataBaseManager.Instance.ItemDataBase.dataList;
        if(itemType == ItemableObjectType.AddItem)
        {
            Debug.Log("Add Item");
            foreach (int i in itemIndexList)
            {
                // Debug.Assert(i <= itemDataList.Count && i>= -1, "Item idx " + i + " is not Defined. Object " + this.ToString());
                ItemData item = itemDataList[i];
                InventoryManager.Instance.AddItem(item);
                WindowManager.Instance.inventoryWindow.UpdateInventory();
            }
        }
        else if (itemType == ItemableObjectType.DeleteItem)
        {
            foreach (int i in itemIndexList)
            {
                // Debug.Assert(i <= itemDataList.Count && i >= -1, "Item idx " + i + " is not Defined. Object " + this.ToString());
                ItemData item = itemDataList[i];
                InventoryManager.Instance.DeleteItem(item);
                WindowManager.Instance.inventoryWindow.UpdateInventory();
            }
        }

        if (deleteAfterInteract)
            this.gameObject.SetActive(false);
    }
}
