using System.Collections.Generic;
using System.IO;
using System.Linq;
using EditorHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace DataBaseScripts.Base
{
    public abstract class DataBase<T> : ScriptableObject where T : DataType
    {
        [SerializeField] protected string databaseName;
#if UNITY_EDITOR
        [ArrayElementTitle("name")]
#endif
        [SerializeField] public List<T> dataList;
        [SerializeField] private bool needToBeWritable;
        public Dictionary<int, T> dataKeyDictionary;

        private void Awake()
        {
            LoadJson();
        }

        public void LoadJson()
        {
            string json;
            string path;
            if (needToBeWritable) // 리소스 바깥
            {
                path = Path.Combine(Application.persistentDataPath, databaseName+".json");
                using (StreamReader file = File.OpenText(path))
                {
                    json = file.ReadToEnd();
                    dataList = JsonConvert.DeserializeObject<List<T>>(json);
                    dataKeyDictionary = dataList.ToDictionary(x => x.idx);
                }
            }
            else // 리소스 안
            {
                path = Path.Combine("IngameData", "Json", databaseName);
                json = Resources.Load<TextAsset>(path).ToString();
                dataList = JsonConvert.DeserializeObject<List<T>>(json);
                dataKeyDictionary = dataList.ToDictionary(x => x.idx);
            }
        }
        public void SaveJson(){
            string path = Path.Combine(Application.dataPath, "Resources", "IngameData", "Json", databaseName + ".json");
            File.WriteAllText(path, JsonConvert.SerializeObject(dataList));
        }
    }
}
