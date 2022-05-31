using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

/*
 * 
 * 아이템 데이터 관리하는 매니저
 * 
 */
[Serializable]
public struct ItemData
{
    public int idx;
    public string name;
    public string itemInfo;
    public string spriteName;
    public string itemDescription;
    public ItemData (int idx, string name, string itemInfo, string spriteName, string itemDescription)
    {
        this.idx = idx;
        this.name = name;
        this.itemInfo = itemInfo;
        this.spriteName = spriteName;
        this.itemDescription = itemDescription;
    }
}
[CreateAssetMenu(fileName = "ItemDataBase", menuName = "ScriptableObjects/ItemDataBase", order = 1)]
public class ItemDataBase : DataBase
{
    public List<ItemData> ItemDataList;

    public override void LoadJson()
    {
        string path = "IngameData/Item";
        TextAsset json = Resources.Load<TextAsset>(path);
        Debug.Log(json.ToString());
        ItemDataList = JsonConvert.DeserializeObject<List<ItemData>>(json.ToString());
    }

    public override void SaveJson()
    {
        
    }
}

[CustomEditor(typeof(ItemDataBase))]
public class ItemDataBaseEditor : DataBaseEditor<ItemDataBase> { }