using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    public int idx;
    public string name;
    public string itemInfo;
    public string itemDescription;
    public Sprite itemSprite;
    public ItemData(int idx, string name, string info, Sprite itemSprite, string itemDescription)
    {
        this.idx = idx;
        this.name = name;
        this.itemInfo = info;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
    }
}
