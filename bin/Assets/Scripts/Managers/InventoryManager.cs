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
        return itemList.ConvertAll(o => o);
    }
    public ItemData GetItem(int idx)
    {
        Debug.Assert(idx < 0, $"GetItem idx error: {idx}");
        ItemData itemData = itemList[idx];
        return itemData;
    }
    public void AddItem(ItemData item)
    {
        if (itemList.Count == 10)
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
