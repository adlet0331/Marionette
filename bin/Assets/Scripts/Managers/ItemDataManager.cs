using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
}
public class ItemDataManager : Singleton<ItemDataManager>
{
    public List<Item> ItemDataList;
    private void Start()
    {
        LoadJson();
    }

    public void LoadJson()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("IngameData/Items");
        ItemDataStruct[] Datas = JsonHelper.FromJson<ItemDataStruct>("{\"resources\":" + jsonData.text + "}");
        ItemDataList = new List<Item>();
        foreach (ItemDataStruct itemDataStruct in Datas)
        {
            Sprite itemSprite = Resources.Load<Sprite>("Sprites/Items/" + itemDataStruct.spriteName);
            ItemDataList.Add(new Item(itemDataStruct.idx, itemDataStruct.name, itemDataStruct.itemInfo, itemSprite));
        }
        return;
    }
}
