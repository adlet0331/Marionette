using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SLManager : Singleton<SLManager>
{
    [SerializeField] private SaveData currentSaveData;

    [Serializable]
    public class SaveData
    {
        public float posx;
        public float posy;

        public string facing;
        public string currentScene;

        public List<int> ItemIdxList;
    }

    public void NewGame()
    {

    }
    public void Save(int idx) 
    {
        string json = JsonUtility.ToJson(currentSaveData);
        string path = Application.persistentDataPath + "/" + "SaveData" + idx;
        File.WriteAllText(path, json);
    }
    public void Load(int idx) 
    {
        string path = Application.persistentDataPath + "/" + "SaveData" + idx;
        if (File.Exists(path)) 
        {
            string json = File.ReadAllText(path);
            currentSaveData = JsonUtility.FromJson<SaveData>(json);
            return;
        }
    }

    private SaveData CreateNewSaveData()
    {
        SaveData newData = new SaveData();

        return newData;
    }

    private void Update()
    {
        
    }

}
