using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InGameObject
{
    public string name;
    public Sprite sprite;
    public int index;
    public List<string> scripts;
    public InGameObject()
    {
        name = "dummy";
        sprite = null;
        index = 0;
        scripts = new List<string>();
        scripts.Add("This is dummy script");
        scripts.Add("This is second dummy script");
    }
    public InGameObject(string name, int index, List<string> scripts)
    {
        this.name = "dummy";
        this.index = 0;
        this.scripts = new List<string>();
        foreach(string script in scripts)
        {
            this.scripts.Add(script);
        }
    }
}

public class InGameObjectManager : Singleton<InGameObjectManager>
{
    public List<InGameObject> ImGameObjectList;
}
