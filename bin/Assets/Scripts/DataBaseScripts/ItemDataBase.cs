using System;
using UnityEditor;
using UnityEngine;

/*
 * 
 * 아이템 데이터 관리하는 매니저
 * 
 */
[Serializable]
public class ItemData : DataType
{
    public int initStatus;
    public string itemInfo;
    public string spriteName;
    public string itemDescription;
}
[CreateAssetMenu(fileName = "ItemDataBase", menuName = "ScriptableObjects/ItemDataBase", order = 1)]
public class ItemDataBase : DataBase<ItemData> { }

[CustomEditor(typeof(ItemDataBase))]
public class ItemDataBaseEditor : DataBaseEditor<ItemDataBase, ItemData> { }