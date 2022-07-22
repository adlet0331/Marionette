using System;
using System.Collections.Generic;
using System.IO;
using DataBaseScripts;
using InGameObjects.Interaction;
using Newtonsoft.Json;
using UnityEngine;
using Random = System.Random;

namespace Managers
{
    public class SLManager : Singleton<SLManager>
    {
        [SerializeField] private SLDataBase sLDataBase;

        [SerializeField] private string currentSaveDataName;
        [SerializeField] private SaveData currentSaveData;

        [Serializable]
        public struct SaveData
        {
            // Player Info
            public string sceneName;
            public int playerPosX;
            public int playerPosY;
            // Settings
            public string setting;
            // Inventory
            public List<ItemData> itemList;
            // Stella List
            public List<int> stellaIdxList;
            // SceneObjects
            public List<bool> interactingObjectStatusList;
        }
        
        public enum SaveDataType
        {
            InteractionObject = 0,
            ItemAdd = 1,
            AnimaObject = 2,
        }

        public void OnNotify(SaveDataType dataType, bool isAdd, int idx)
        {
            if (dataType == SaveDataType.InteractionObject)
            {
                currentSaveData.interactingObjectStatusList[idx] = isAdd;
                return;
            }
            else if (dataType == SaveDataType.ItemAdd)
            {
                
            }
            else if (dataType == SaveDataType.AnimaObject)
            {
                
            }
        }
        
        public void InitSaveData()
        {
            SaveData newData = new SaveData();

            newData.sceneName = SceneSwitchManager.SceneName.Girl_room.ToString();
            newData.playerPosX = 0;
            newData.playerPosY = 0;

            newData.setting = "init";
        
            newData.itemList = InventoryManager.Instance.GetItemList();

            newData.interactingObjectStatusList = new List<bool>();

            foreach (InteractionData var in DataBaseManager.Instance.interactionDataBase.dataList)
            {
                newData.interactingObjectStatusList.Add(var.initStatus);
            }
            
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
                    File.WriteAllText(sLDataPath, JsonConvert.SerializeObject(sLDataBase.dataList));
                    return;
                }
            }
            // New
            saveCurrentFileAsJson();
            SLData newData = new SLData();
            newData.idx = slotIndex;
            newData.name = currentSaveDataName;
            sLDataBase.dataList.Add(newData);
            File.WriteAllText(sLDataPath, JsonConvert.SerializeObject(sLDataBase.dataList));
        }

        public void Load()
        {
            this.Load(1);
        }
        public void Load(int index)
        {
            sLDataBase.LoadJson();
            currentSaveDataName = sLDataBase.dataList[index].name;
            string saveDataPath = Path.Combine(Application.dataPath, "Resources", "IngameData", "Json", "SaveData", $"{currentSaveDataName}.json");
            string json = File.ReadAllText(saveDataPath);
            Debug.Log(json.ToString());
            currentSaveData = JsonConvert.DeserializeObject<SaveData>(json.ToString());
        }
        
        private void saveCurrentFileAsJson()
        {
            string path = Path.Combine(Application.dataPath, "Resources", "IngameData", "Json", "SaveData", $"{currentSaveDataName}.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(currentSaveData));
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
                var gameObjectStatus = this.currentSaveData.interactingObjectStatusList[interactingObject.Idx];
                interactingObject.gameObject.SetActive(gameObjectStatus);
            }
        }
    }
}
