using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataBaseScripts;
using EditorHelper;
using InGameObjects.Interaction;
using Newtonsoft.Json;
using UI;
using UnityEngine;
using Random = System.Random;

namespace Managers
{
    [Serializable]
    public class InteractionStatus
    {
        public int Idx;
        public bool CurrentStatus;

        public InteractionStatus(int idx, bool currentStatus)
        {
            this.Idx = idx;
            this.CurrentStatus = currentStatus;
        }
    }
    public class SLManager : Singleton<SLManager>
    {
        [SerializeField] private SLDataBase sLDataBase;

        [SerializeField] private string currentSaveDataName;
        [SerializeField] private SaveData currentSaveData;

        [Serializable]
        public struct SaveData
        {
            // Player Info
            public string sceneString;
            public SceneSwitchManager.SceneName sceneName;
            public float playerLocalPosX;
            public float playerLocalPosY;
            public float timePassed;
            // Settings
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

        public Vector3 PlayerPosVec => new Vector3(currentSaveData.playerLocalPosX, currentSaveData.playerLocalPosX, 0);

        public void OnNotify(bool activate, int idx)
        {
            currentSaveData.interactingObjectStatusDictionary[idx].CurrentStatus = activate;
        }

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
        
        public void InitSaveData()
        {
            SaveData newData = new SaveData();

            newData.sceneString = "서측 1실험동 - 소녀의 방";
            newData.sceneName = SceneSwitchManager.SceneName.Girl_room;
            currentSaveData.playerLocalPosX = 0;
            currentSaveData.playerLocalPosY = 0;
            newData.timePassed = 0.0f;

            newData.setting = "init";
        
            newData.itemList = new List<ItemData>();
            InventoryManager.Instance.Load(newData.itemList);

            newData.stellaInfoList = new List<StellaInfo>();
            var stellaDataBaseDict = DataBaseManager.Instance.stellaDataBase.dataKeyDictionary;
            foreach (var stellaData in stellaDataBaseDict)
            {
                var newStellaInfo = new StellaInfo();
                newStellaInfo.idx = stellaData.Value.idx;
                newStellaInfo.name = stellaData.Value.name;
                newStellaInfo.spriteName = stellaData.Value.spriteName;
                newStellaInfo.descriptionList = stellaData.Value.descriptionList;
                newStellaInfo.level = 0;
                newStellaInfo.exp = 0;
                newData.stellaInfoList.Add(newStellaInfo);
            }
            StellaManager.Instance.Load(newData.stellaInfoList);

            var interactingObjectStatusList = new List<InteractionStatus>();

            foreach (var var in DataBaseManager.Instance.interactionDataBase.dataList)
            {
                interactingObjectStatusList.Add(new InteractionStatus(var.idx, var.initStatus));
            }

            newData.interactingObjectStatusDictionary = interactingObjectStatusList.ToDictionary(x => x.Idx);

            this.currentSaveData = newData;
            this.currentSaveDataName = createRandomString(10);
        }
        
        /// <summary>
        /// Save to slot {index}. Overwrite data
        /// </summary>
        /// <param name="index"></param>
        public void Save(int index) 
        {
            Debug.Log("SAVE");
            string sLDataPath = Path.Combine(Application.persistentDataPath, "SaveData.json");
            if (!File.Exists(sLDataPath))
            {
                string defaultPath = Path.Combine("IngameData", "Json", "SaveData");
                string defaultSaveData = Resources.Load<TextAsset>(defaultPath).ToString();
                File.WriteAllText(sLDataPath, defaultSaveData);
            }
            sLDataBase.LoadJson();
            saveCurrentStatus();
            List<SLData> saveData = sLDataBase.dataList;
            int slotIndex;
            for (slotIndex = 0; slotIndex < sLDataBase.dataList.Count; slotIndex++)
            {
                // Already Exist
                if (sLDataBase.dataList[slotIndex].idx == index)
                {
                    File.Delete(Path.Combine(Application.persistentDataPath, $"{sLDataBase.dataList[slotIndex].name}.json"));
                    saveCurrentFileAsJson();
                    sLDataBase.dataList[slotIndex].name = currentSaveDataName;
                    File.WriteAllText(sLDataPath, JsonConvert.SerializeObject(sLDataBase.dataList, Formatting.Indented));
                    return;
                }
            }
            // New
            saveCurrentFileAsJson();
            SLData newData = new SLData();
            newData.idx = slotIndex;
            newData.name = currentSaveDataName;
            sLDataBase.dataList.Add(newData);
            File.WriteAllText(sLDataPath, JsonConvert.SerializeObject(sLDataBase.dataList, Formatting.Indented));
        }
        
        public void Load(int index)
        {
            sLDataBase.LoadJson();
            currentSaveDataName = sLDataBase.dataList[index].name;
            string saveDataPath = Path.Combine(Application.persistentDataPath, $"{currentSaveDataName}.json");
            string json = File.ReadAllText(saveDataPath);
            Debug.Log(json);
            currentSaveData = JsonConvert.DeserializeObject<SaveData>(json);
            InventoryManager.Instance.Load(currentSaveData.itemList);
            StellaManager.Instance.Load(currentSaveData.stellaInfoList);

            SceneSwitchManager.Instance.SwitchScene(currentSaveData.sceneName, -1);
        }

        private void saveCurrentStatus()
        {
            currentSaveData.sceneName = SceneSwitchManager.Instance.currentScene;
            currentSaveData.sceneString = SceneSwitchManager.Instance.CurrentSceneInfo.sceneString;
            currentSaveData.playerLocalPosX = PlayerManager.Instance.moveablePlayerObject.transform.localPosition.x;
            currentSaveData.playerLocalPosY = PlayerManager.Instance.moveablePlayerObject.transform.localPosition.y;
            currentSaveData.timePassed += Time.time;

            currentSaveData.itemList = InventoryManager.Instance.GetItemList();
            currentSaveData.stellaInfoList = StellaManager.Instance.GetStellaInfoList();
        }
        
        private void saveCurrentFileAsJson()
        {
            string path = Path.Combine(Application.persistentDataPath, $"{currentSaveDataName}.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(currentSaveData, Formatting.Indented));
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

        public void InitsceneInteractingObjects(InteractingObject[] interactingObjects)
        {
            foreach (var interactingObject in interactingObjects)
            {
                var gameObjectStatus = currentSaveData.interactingObjectStatusDictionary[interactingObject.Idx].CurrentStatus;
                interactingObject.gameObject.SetActive(gameObjectStatus);
            }
        }

        private void Start()
        {
            sLDataBase = Resources.Load(Path.Combine("DataBase", "SLDataBase"), typeof(SLDataBase)) as SLDataBase;
            sLDataBase.dataList = new List<SLData>();
            sLDataBase.dataKeyDictionary = new Dictionary<int, SLData>();
        }
    }
}
