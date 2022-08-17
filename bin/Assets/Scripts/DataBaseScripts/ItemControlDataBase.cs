using System;
using System.Collections.Generic;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

namespace DataBaseScripts
{
    [Serializable]
    public class ItemControlData : DataType
    {
        public List<int> itemIdxList;
        public List<int> itemNumList;
        public List<bool> isAddList;
        public string getDescription;
    };

    [CreateAssetMenu(fileName = "8_ItemControlDataBase", menuName = "ScriptableObjects/8.ItemControlDataBase", order = 8)]
    public class ItemControlDataBase : DataBase<ItemControlData>
    {
    }

    [CustomEditor(typeof(ItemControlDataBase))]
    public class ItemControlDataBaseEditor : DataBaseEditor<ItemControlDataBase, ItemControlData>
    {
    }
}