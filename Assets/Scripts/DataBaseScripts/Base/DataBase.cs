using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Collections.Generic;

namespace DataBaseScripts.Base
{
    public abstract class DataBase<T> : ScriptableObject where T : DataType
    {
        [SerializeField] protected string databaseName;
        public List<T> dataList;
        [SerializeField] private bool needToBeWritable;
        public Dictionary<int, T> dataKeyDictionary;
        public UDictionary<int, T> dataKeyUDictionary;

        private void Awake()
        {
            LoadJson();
        }

        public void LoadJson()
        {
            string json;
            string path;
            if (needToBeWritable) // 프로젝트 바깥, 기기에 저장될 아이들
            {
                path = Path.Combine(Application.persistentDataPath, databaseName+".json");
                using (StreamReader file = File.OpenText(path))
                {
                    json = file.ReadToEnd();
                    dataList = JsonConvert.DeserializeObject<List<T>>(json);
                    dataKeyDictionary = dataList.ToDictionary(x => x.idx);
                    dataKeyUDictionary = new UDictionary<int, T>(dataKeyDictionary);
                }
            }
            else // 프로젝트 안, 변하지 않을 데이터, StaticData
            {
                path = Path.Combine("IngameData", "Json", databaseName);
                json = Resources.Load<TextAsset>(path).ToString();
                dataList = JsonConvert.DeserializeObject<List<T>>(json);
                dataKeyDictionary = dataList.ToDictionary(x => x.idx);
                dataKeyUDictionary = new UDictionary<int, T>(dataKeyDictionary);
            }
        }
        public void SaveJson(){
            string path = Path.Combine(Application.dataPath, "Resources", "IngameData", "Json", databaseName + ".json");
            File.WriteAllText(path, JsonConvert.SerializeObject(dataList));
        }
    }
}
