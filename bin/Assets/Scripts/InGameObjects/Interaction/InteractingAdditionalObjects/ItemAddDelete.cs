using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    [Serializable]
    public class ItemAddDeleteData
    {
        public int itemIdx;
        public int num;
        public bool isAdding;
    }

    [Serializable]
    public class ItemAddDelete : AbstractInteractionObject
    {
        [SerializeField] private List<ItemAddDeleteData> dataList;
        public override void Interact()
        {
            foreach (var itemAddDeleteData in dataList)
            {
                if (itemAddDeleteData.isAdding)
                {
                    InventoryManager.Instance.AddItem(itemAddDeleteData.itemIdx, itemAddDeleteData.num);
                }
                else
                {
                    InventoryManager.Instance.DeleteItem(itemAddDeleteData.itemIdx, itemAddDeleteData.num);
                }
            }
        }
    }
}