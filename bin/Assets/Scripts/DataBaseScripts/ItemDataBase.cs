using System;
using DataBaseScripts.Base;
using UnityEditor;
using UnityEngine;

/*
 * 
 * 아이템 데이터 관리하는 매니저
 * 
 */
namespace DataBaseScripts
{
    [Serializable]
    public class ItemData : DataType
    {
        public string itemInfo;
        public string spriteName;
    }

    [CreateAssetMenu(fileName = "ItemDataBase", menuName = "ScriptableObjects/ItemDataBase", order = 100)]
    public class ItemDataBase : DataBase<ItemData>
    {
    }

    [CustomEditor(typeof(ItemDataBase))]
    public class ItemDataBaseEditor : DataBaseEditor<ItemDataBase, ItemData>
    {
    }
}