using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public string name;
    public int idx;
    public Sprite itemSprite;
    public string itemInfo;
    public Item(Item item)
    {
        this.idx = item.idx;
        this.name = item.name;
        this.itemSprite = item.itemSprite;
        this.itemInfo = item.itemInfo;
    }
}
