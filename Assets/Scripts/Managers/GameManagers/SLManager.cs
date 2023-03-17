using System;
using System.Collections.Generic;
using System.IO;
using DataBaseScripts;
using EditorHelper;
using Newtonsoft.Json;
using UI;
using UnityEngine;
using Random = System.Random;

namespace Managers
{
    [Serializable]
    public struct SaveData
    {
        // Player Info
        public string sceneString;
        public SceneInfo sceneInfo;
        public float playerLocalPosX;
        public float playerLocalPosY;
        public float timePassed;
        // Settings : TODO
        public string setting;
        // Inventory
        public List<ItemData> itemList;
        // Stella List
#if UNITY_EDITOR
        [ArrayElementTitle("name")]
#endif
        public List<StellaInfo> stellaInfoList;
        // SceneObjects
        public Dictionary<int, InteractionStatus> interactingObjectStatusDictionary;
    }
    [Serializable]
    public class SLManager : AGameManager
    {
        [SerializeField] private SLDataBase sLDataBase;

        [SerializeField] private string currentSaveDataName;
        [Header("For Debugging")]
        [SerializeField] private SaveData currentSaveData;

        public Vector3 PlayerPosVec => new Vector3(currentSaveData.playerLocalPosX, currentSaveData.playerLocalPosY, 0);

        public SaveInfo GetSaveDataInfo(int index)
        {
            if (sLDataBase || !sLDataBase.dataKeyDictionary.ContainsKey(index))
            {
                return new SaveInfo("", 0.0f);
            }
            else
            {
                sLDataBase.LoadJson();
                currentSaveDataName = sLDataBase.dataList[index].name;
                string saveDataPath = Path.Combine(Application.persistentDataPath, $"{currentSaveDataName}.json");
                string json = File.ReadAllText(saveDataPath);
                Debug.Log(json);
                var saveInfo = JsonConvert.DeserializeObject<SaveInfo>(json);
                return saveInfo;
            }
        }
        
        public void InitSaveData(SaveData initData)
        {
            currentSaveData = initData;
            currentSaveDataName = createRandomString(10);
        }
        
        /// <summary>
        /// Save to slot {index}. Overwrite data
        /// </summary>
        /// <param name="index"></param>
        public void SaveData(int index, SaveData saveData)
        {
            currentSaveData = saveData;
            string sLDataPath = Path.Combine(Application.persistentDataPath, "SaveData.json");
            if (!File.Exists(sLDataPath))
            {
                string defaultPath = Path.Combine("IngameData", "Json", "SaveData");
                string defaultSaveData = Resources.Load<TextAsset>(defaultPath).ToString();
                File.WriteAllText(sLDataPath, defaultSaveData);
            }
            sLDataBase.LoadJson();
            List<SLData> dataList = sLDataBase.dataList;
            int slotIndex;
            for (slotIndex = 0; slotIndex < dataList.Count; slotIndex++)
            {
                // Already Exist
                if (dataList[slotIndex].idx == index)
                {
                    File.Delete(Path.Combine(Application.persistentDataPath, $"{dataList[slotIndex].name}.json"));
                    saveDataAsJson(saveData);
                    dataList[slotIndex].name = currentSaveDataName;
                    File.WriteAllText(sLDataPath, JsonConvert.SerializeObject(dataList, Formatting.Indented));
                    return;
                }
            }
            // New
            saveDataAsJson(saveData);
            SLData newData = new SLData();
            newData.idx = slotIndex;
            newData.name = currentSaveDataName;
            dataList.Add(newData);
            File.WriteAllText(sLDataPath, JsonConvert.SerializeObject(dataList, Formatting.Indented));
        }
        
        public SaveData LoadSavedData(int index)
        {
            sLDataBase.LoadJson();
            currentSaveDataName = sLDataBase.dataList[index].name;
            string saveDataPath = Path.Combine(Application.persistentDataPath, $"{currentSaveDataName}.json");
            string json = File.ReadAllText(saveDataPath);
            SaveData loadedData = JsonConvert.DeserializeObject<SaveData>(json);
 
            return loadedData;
        }

        private void saveDataAsJson(SaveData saveData)
        {
            string path = Path.Combine(Application.persistentDataPath, $"{currentSaveDataName}.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(saveData, Formatting.Indented));
        }

        private string createRandomString(int length)
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[length];
            var random = new Random();
            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }
            return new string(Charsarr);
        }

        public override void Start()
        {
            sLDataBase = Resources.Load(Path.Combine("DataBase", "SLDataBase"), typeof(SLDataBase)) as SLDataBase;
            Debug.Assert(sLDataBase != null, "No SLDataBase In Project.\n Create Scriptable Object \"ScriptableObjects/SLDataBase\" in Project's Right Clilck");
            sLDataBase.dataList = new List<SLData>();
            sLDataBase.dataKeyDictionary = new Dictionary<int, SLData>();
        }
    }
}
