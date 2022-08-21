using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataBaseScripts;
using InGameObjects.Interaction;
using Newtonsoft.Json;
using SerializableManager;
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
            public Vector3 playerLocalPos;
            // Settings
            public string setting;
            // Inventory
            public List<ItemData> itemList;
            // Stella List
            [ArrayElementTitle("name")]
            public List<StellaInfo> stellaInfoList;
            // SceneObjects
            public Dictionary<int, InteractionStatus> interactingObjectStatusDictionary;
        }
        
        public void OnNotify(bool disableAfterInteract, int idx)
        {
            currentSaveData.interactingObjectStatusDictionary[idx].CurrentStatus = !disableAfterInteract;
        }
        
        public void InitSaveData()
        {
            SaveData newData = new SaveData();

            newData.sceneName = SceneSwitchManager.SceneName.Girl_room;
            currentSaveData.playerLocalPos = new Vector3(0, 0, 0);

            newData.setting = "init";
        
            newData.itemList = new List<ItemData>();
            InventoryManager.Instance.Load(newData.itemList);

            newData.stellaInfoList = new List<StellaInfo>();
            var stellaDataBaseDict = DataBaseManager.Instance.stellaDataBase.dataKeyDictionary;
            foreach (var stellaData in stellaDataBaseDict)
            {
                newData.stellaInfoList.Add(new StellaInfo(stellaData.Value, 0, 0));
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
            string sLDataPath = Path.Combine(Application.dataPath, "Resources", "IngameData", "Json", "SaveData.json");
            TextAsset json = Resources.Load<TextAsset>(sLDataPath);
            sLDataBase.LoadJson();
            saveCurrentStatus();
            List<SLData> saveData = sLDataBase.dataList;
            int slotIndex;
            for (slotIndex = 0; slotIndex < sLDataBase.dataList.Count; slotIndex++)
            {
                // Already Exist
                if (sLDataBase.dataList[slotIndex].idx == index)
                {
                    File.Delete(Path.Combine(Application.dataPath, "Resources", "IngameData", "Json", "SaveData", $"{sLDataBase.dataList[slotIndex].name}.json"));
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
            string saveDataPath = Path.Combine(Application.dataPath, "Resources", "IngameData", "Json", "SaveData", $"{currentSaveDataName}.json");
            string json = File.ReadAllText(saveDataPath);
            Debug.Log(json);
            currentSaveData = JsonConvert.DeserializeObject<SaveData>(json);

            SceneSwitchManager.Instance.SwitchScene(currentSaveData.sceneName, -1);
        }

        private void saveCurrentStatus()
        {
            currentSaveData.sceneName = SceneSwitchManager.Instance.currentScene;
            currentSaveData.sceneString = SceneSwitchManager.Instance.CurrentSceneInfo.sceneString;
            currentSaveData.itemList = InventoryManager.Instance.GetItemList();
            currentSaveData.stellaInfoList = StellaManager.Instance.GetStellaInfoList();

            currentSaveData.playerLocalPos = PlayerManager.Instance.moveablePlayerObject.transform.localPosition;
        }
        
        private void saveCurrentFileAsJson()
        {
            string path = Path.Combine(Application.dataPath, "Resources", "IngameData", "Json", "SaveData", $"{currentSaveDataName}.json");
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
    }
}
