using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Random = System.Random;

public class SLManager : Singleton<SLManager>
{
    [SerializeField] private SLDataBase sLDataBase;
    [SerializeField] private ItemDataBase itemDataBase;
    [SerializeField] private ScriptDataBase scriptDataBase;
    [SerializeField] private LockDataBase lockDataBase;

    [SerializeField] private string currentSaveDataName;
    [SerializeField] private SaveData currentSaveData;
    
    [Serializable]
    public enum SceneObjectStatus
    {
        NotAvaliable = 0,
        Interactable = 1,
        Showable = 2,
    }

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
        // SceneObjects
        public List<SceneObjectStatus> sceneItemObjectStatusList;
        public List<SceneObjectStatus> sceneScriptObjectStatusList;
        public List<SceneObjectStatus> sceneLockObjectStatusList;
    }

    private void saveCurrentFileAsJson()
    {
        string path = $"IngameData/SaveData/{currentSaveDataName}.json";
        string json = JsonConvert.SerializeObject(currentSaveData);
        File.WriteAllTextAsync(path, json);
    }
    public void Save() 
    {
        Debug.Log("SAVE");
        string path = "IngameData/SaveData.json";
        TextAsset json = Resources.Load<TextAsset>(path);
        sLDataBase.LoadJson();
        List<SLData> saveData = sLDataBase.dataList;
        foreach (SLData var in saveData)
        {
            if (var.name == currentSaveDataName)
            {
                saveCurrentFileAsJson();
                return;
            }
        }
        SLData newData = new SLData();
        newData.idx = saveData.Count;
        newData.name = currentSaveDataName;
        sLDataBase.dataList.Add(newData);
        path = Path.Combine(Application.dataPath, "Resources/" + path);
        File.WriteAllText(path, JsonConvert.SerializeObject(saveData));
    }
    public void Load()
    {
        return;
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
    public SaveData InitSaveData(bool setCurrentSaveData)
    {
        SaveData newData = new SaveData();

        newData.sceneName = SceneSwitchManager.SceneName.P_Girl_room.ToString();
        newData.playerPosX = 0;
        newData.playerPosY = 0;

        newData.setting = "init";
        
        newData.itemList = InventoryManager.Instance.GetItemList();

        newData.sceneItemObjectStatusList = new List<SceneObjectStatus>();
        newData.sceneScriptObjectStatusList = new List<SceneObjectStatus>();
        newData.sceneLockObjectStatusList = new List<SceneObjectStatus>();
        
        foreach (ItemData var in itemDataBase.dataList)
        {
            newData.sceneItemObjectStatusList.Add((SceneObjectStatus)var.initStatus);
        }
        foreach (ScriptData var in scriptDataBase.dataList)
        {
            newData.sceneScriptObjectStatusList.Add((SceneObjectStatus)var.initStatus);
        }
        foreach (LockData var in lockDataBase.dataList)
        {
            newData.sceneLockObjectStatusList.Add((SceneObjectStatus)var.initStatus);
        }

        if (setCurrentSaveData)
        {
            this.currentSaveData = newData;
            this.currentSaveDataName = createRandomString(10);
        }

        return newData;
    }

    private void Update()
    {
        
    }

}
