using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public abstract class DataBase<T> : ScriptableObject
{
    [SerializeField] protected string databaseName;
    [SerializeField] public List<T> dataList;
    public void LoadJson()
    {
        string path = $"IngameData/{databaseName}";
        TextAsset json = Resources.Load<TextAsset>(path);
        Debug.Log(json.ToString());
        dataList = JsonConvert.DeserializeObject<List<T>>(json.ToString());
        return;
    }
    public void SaveJson(){
        return;
    }
}
