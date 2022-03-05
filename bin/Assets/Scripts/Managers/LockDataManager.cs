using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LockData
{
    public int idx;
    public string interactName;
    public string interactString;
    public List<LockInfo> lockInfoList;
    public LockData(int idx, string name, string str, List<LockInfo> lst)
    {
        this.idx = idx;
        this.interactName = name;
        this.interactString = str;
        this.lockInfoList = lst;
    }
};

[Serializable]
public struct LockDataFromJson
{
    public int idx;
    public string name;
    public string firstInteractString;
    public int[] needItemIdxArray;
    public string[] noItemString;
}

public class LockDataManager : Singleton<LockDataManager>
{
    public List<LockData> LockDataList;

    private void Start()
    {
        LoadJson();
    }

    public void LoadJson()
    {
        LockDataList = new List<LockData>();

        TextAsset jsonData = Resources.Load<TextAsset>("IngameData/LockObj");
        LockDataFromJson[] Datas = JsonHelper.FromJson<LockDataFromJson>("{\"resources\":" + jsonData.text + "}");
        foreach (LockDataFromJson lockDataFromJson in Datas)
        {
            List<LockInfo> lst = new List<LockInfo>();
            for (int i=0; i<lockDataFromJson.needItemIdxArray.Length; i++)
            {
                lst.Add(new LockInfo(lockDataFromJson.needItemIdxArray[i], lockDataFromJson.noItemString[i]));
            }
            LockDataList.Add(new LockData(lockDataFromJson.idx, lockDataFromJson.name, lockDataFromJson.firstInteractString, lst));
        }
        return;
    }
}
