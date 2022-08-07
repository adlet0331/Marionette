using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SerializableManager;
using UnityEngine;

namespace DataBaseScripts.Base
{
    public abstract class DataBase<T> : ScriptableObject
    {
        [SerializeField] protected string databaseName;
        [ArrayElementTitle("name")]
        [SerializeField] public List<T> dataList;

        private void Awake()
        {
            LoadJson();
        }

        public void LoadJson()
        {
            string path = Path.Combine("IngameData", "Json", databaseName);
            TextAsset json = Resources.Load<TextAsset>(path);
            dataList = JsonConvert.DeserializeObject<List<T>>(json.ToString());
        }
        public void SaveJson(){
            string path = Path.Combine(Application.dataPath, "Resources", "IngameData", "Json", databaseName + ".json");
            File.WriteAllText(path, JsonConvert.SerializeObject(dataList));
        }
    }
}
