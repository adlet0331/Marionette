using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

/*
 * 
 * 아이템 데이터 관리하는 매니저
 * 
 */
[Serializable]
public class ItemDataStruct
{
    public int idx;
    public string name;
    public string itemInfo;
    public string spriteName;
    public string itemDescription;
}
[CreateAssetMenu(fileName = "ItemDataBase", menuName = "ScriptableObjects/ItemDataBase", order = 1)]
public class ItemDataBase : DataBase
{
    public List<ItemData> ItemDataList;

    public override void LoadJson()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("IngameData/Items");
        ItemDataStruct[] Datas = JsonHelper.FromJson<ItemDataStruct>("{\"resources\":" + jsonData.text + "}");
        ItemDataList = new List<ItemData>();
        foreach (ItemDataStruct itemDataStruct in Datas)
        {
            Sprite itemSprite = Resources.Load<Sprite>("Sprites/Items/" + itemDataStruct.spriteName);
            ItemDataList.Add(new ItemData(itemDataStruct.idx, itemDataStruct.name, itemDataStruct.itemInfo, itemSprite, itemDataStruct.itemDescription));
        }
        return;
    }

    public override void SaveJson()
    {
        foreach (ItemData itemDataStruct in ItemDataList){
            JObject itemJson = new JObject(
                new JProperty("idx", itemDataStruct.idx),
                new JProperty("name", itemDataStruct.name),
                new JProperty("itemInfo", itemDataStruct.itemInfo),
                new JProperty("spriteName", itemDataStruct.itemSprite),
                new JProperty("itemDescription", itemDataStruct.itemDescription)
            );
        }
    }
}

[CustomEditor(typeof(ItemDataBase))]
public class ItemDataBaseEditor : DataBaseEditor<ItemDataBase> { }