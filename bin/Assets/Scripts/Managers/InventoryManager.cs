using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private List<Item> itemList;
    public List<Item> GetItemList()
    {
        return itemList.ConvertAll(o => new Item(o.idx, o.name, o.itemInfo, o.itemSprite));
    }
    public void AddItem(Item item)
    {
        itemList.Add(item);
        return;
    }
    public void DeleteItem(Item item)
    {
        foreach(Item it in itemList)
        {
            if (it.idx == item.idx)
            {
                itemList.Remove(it);
            }
        }
        return;
    }
}
