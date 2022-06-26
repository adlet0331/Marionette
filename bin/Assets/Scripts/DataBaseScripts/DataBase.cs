using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public abstract class DataBase<T> : ScriptableObject
{
    [SerializeField] protected string databaseName;
    [SerializeField] public List<T> dataList;
    public void LoadJson()
    {
        string path = $"IngameData/{databaseName}";
        TextAsset json = Resources.Load<TextAsset>(path);
        dataList = JsonConvert.DeserializeObject<List<T>>(json.ToString());
    }
    public void SaveJson(){
        string path = Path.Combine(Application.dataPath, $"Resources/IngameData/{databaseName}.json");
        File.WriteAllText(path, JsonConvert.SerializeObject(dataList));
    }
}
