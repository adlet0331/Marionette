using System;
using System.Collections.Generic;
using DataBaseScripts;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class InventoryManager : AGameManager
    {
        [SerializeField] private List<ItemData> inventoryItemList;
        public List<ItemData> InventoryItemList => new List<ItemData>(inventoryItemList);

        public List<ItemData> InitInventoryList()
        {
            inventoryItemList = new List<ItemData>();
            return new List<ItemData>(inventoryItemList);
        }

        public void LoadInventoryList(List<ItemData> list)
        {
            inventoryItemList = list;
        }
        
        public bool CheckItemInInventory(int idx)
        {
            foreach (ItemData item in inventoryItemList)
            {
                if (idx == item.idx)
                    return true;
            }
            return false;
        }

        public int GetItemNumInInventory(int idx)
        {
            int num = 0;
            foreach (ItemData item in inventoryItemList)
            {
                if (idx == item.idx)
                    num += 1;
            }

            return num;
        }
        public ItemData GetItemDataWithIdx(int inventoryIdx)
        {
            Debug.Assert(inventoryIdx < 0, $"GetItem Idx error: {inventoryIdx}");
            ItemData itemData = inventoryItemList[inventoryIdx];
            return itemData;
        }
        public void AddItem(int itemIdx, int num)
        {
            var item = GamePlayManager.Instance.dataBaseCollection.itemDataBase.dataKeyDictionary[itemIdx];
            for (int i = 0; i < num; i++)
            {
                inventoryItemList.Add(item);
            }
        }
        
        public bool DeleteItem(int itemIdx, int num)
        {
            var cnt = 0;
            for (int i = 0; i < inventoryItemList.Count; i++)
            {
                if (inventoryItemList[i].idx == itemIdx)
                {
                    cnt += 1;
                    // 전부 있어야 Remove, 이외에는 false
                    if (num == cnt)
                    {
                        for (int j = 0; j < num; j++)
                        {
                            inventoryItemList.Remove(inventoryItemList[i]);
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        public override void Start()
        {
            
        }
    }
}
