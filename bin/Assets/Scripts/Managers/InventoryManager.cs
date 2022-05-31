using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private List<ItemData> itemList;

    public bool CheckItemIsIn(int idx)
    {
        foreach (ItemData item in itemList)
        {
            if (idx == item.idx)
                return true;
        }
        return false;
    }
    public List<ItemData> GetItemList()
    {
        return itemList.ConvertAll(o => new ItemData(o.idx, o.name, o.itemInfo, o.itemSprite, o.itemDescription));
    }
    public ItemData GetItem(int idx)
    {
        if (idx < 0)
        {
            return null;
        }
        else
        {
            ItemData itemData = itemList[idx];
            return itemData;
        }
    }
    public void AddItem(ItemData item)
    {
        if (itemList.Count == 10)
            // �� á�ٴ� ����? �������
            return;

        itemList.Add(item);
        return;
    }
    public void DeleteItem(ItemData item)
    {
        foreach(ItemData it in itemList)
        {
            if (it.idx == item.idx)
            {
                itemList.Remove(it);
                return;
            }
        }
        return;
    }
}
