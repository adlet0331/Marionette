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
            public int playerPosX;
            public int playerPosY;
            // Settings
            public string setting;
            // Inventory
            public List<ItemData> itemList;
            // Stella List
            public List<int> stellaIdxList;
            // SceneObjects
            public Dictionary<int, InteractionStatus> interactingObjectStatusDictionary;
        }

        public void OnNotify(bool disableAfterInteract, int idx)
        {
            var groupInteraction = FindObjectOfType<GroupInteraction>();
            
            currentSaveData.interactingObjectStatusDictionary[idx].CurrentStatus = !disableAfterInteract;
        }
        
        public void InitSaveData()
        {
            SaveData newData = new SaveData();

            newData.sceneName = SceneSwitchManager.SceneName.Girl_room;
            newData.playerPosX = 0;
            newData.playerPosY = 0;

            newData.setting = "init";
        
            newData.itemList = InventoryManager.Instance.GetItemList();

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
