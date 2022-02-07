using System;
using UnityEngine;

[Serializable]
public class Item
{
    public int idx;
    public string name;
    public string itemInfo;
    public Sprite itemSprite;
    public Item(int idx, string name, string info, Sprite itemSprite)
    {
        this.idx = idx;
        this.name = name;
        this.itemInfo = info;
        this.itemSprite = itemSprite;
    }
}
