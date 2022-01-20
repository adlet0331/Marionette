using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public int id;
    public string name;
    public Sprite itemSprite;
    public string itemInfo;
    public Item(Item item)
    {
        this.id = item.id;
        this.name = item.name;
        this.itemSprite = item.itemSprite;
        this.itemInfo = item.itemInfo;
    }
}
