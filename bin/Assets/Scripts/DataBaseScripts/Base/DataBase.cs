using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SerializableManager;
using UnityEngine;

namespace DataBaseScripts.Base
{
    public abstract class DataBase<T> : ScriptableObject where T : DataType
    {
        [SerializeField] protected string databaseName;
        [ArrayElementTitle("name")]
        [SerializeField] public List<T> dataList;
        public Dictionary<int, T> dataKeyDictionary;

        private void Awake()
        {
            LoadJson();
        }

        public void LoadJson()
        {
            string path = Path.Combine("IngameData", "Json", databaseName);
            TextAsset json = Resources.Load<TextAsset>(path);
            dataList = JsonConvert.DeserializeObject<List<T>>(json.ToString());
            dataKeyDictionary = dataList.ToDictionary(x => x.idx);
        }
        public void SaveJson(){
            string path = Path.Combine(Application.dataPath, "Resources", "IngameData", "Json", databaseName + ".json");
            File.WriteAllText(path, JsonConvert.SerializeObject(dataList));
        }
    }
}
