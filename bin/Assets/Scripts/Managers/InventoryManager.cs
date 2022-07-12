using System.Collections.Generic;
using DataBaseScripts;
using UnityEngine;

namespace Managers
{
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
        public void AddItem(int itemIdx, int num)
        {
            var item = DataBaseManager.Instance.ItemDataBase.dataList[itemIdx];
            for (int i = 0; i < num; i++)
            {
                itemList.Add(item);
            }
        }

        public bool DeleteSingleItem(int itemIdx)
        {
            if (CheckItemIsIn(itemIdx))
            {
                foreach (var itemData in itemList)
                {
                    if (itemData.idx == itemIdx)
                    {
                        itemList.Remove(itemData);
                        return true;
                    }
                }
            }
            return false;
        }
        public bool DeleteItem(int itemIdx, int num)
        {
            var cnt = 0;
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].idx == itemIdx)
                {
                    cnt += 1;
                    if (num == cnt)
                    {
                        for (int j = 0; j < num; j++)
                        {
                            DeleteSingleItem(itemIdx);
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
