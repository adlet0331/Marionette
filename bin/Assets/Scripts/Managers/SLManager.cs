using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLManager : Singleton<SLManager>
{
    [SerializeField] private string sceneName;

    public class SaveData
    {
        float posx;
        float posy;

        string facing;
        string currentScene;
    }

    private void NewGame()
    {

    }

    private void LoadGame(int sectorN)
    {

    }

    private SaveData CreateNewSaveData()
    {
        SaveData newData = new SaveData();

        return newData;
    }

    private void Update()
    {
        
    }
    private void SaveAll()
    {

    }
    private void LoadAll()
    {

    }

}
