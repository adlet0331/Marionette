using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
public class LockData
{
    public int idx;
    public string name;
    public string interactString;
    public List<LockInfo> lockInfoList;
    public LockData(int idx, string name, string str, List<LockInfo> lst)
    {
        this.idx = idx;
        this.name = name;
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
[CreateAssetMenu(fileName = "LockDataBase", menuName = "ScriptableObjects/LockDataBase", order = 1)]
public class LockDataBase : DataBase
{
    public List<LockData> LockDataList;

    public override void LoadJson()
    {
        LockDataList = new List<LockData>();

        TextAsset jsonData = Resources.Load<TextAsset>("IngameData/LockObj");
        LockDataFromJson[] Datas = JsonHelper.FromJson<LockDataFromJson>("{\"resources\":" + jsonData.text + "}");
        foreach (LockDataFromJson lockDataFromJson in Datas)
        {
            List<LockInfo> lst = new List<LockInfo>();
            for (int i = 0; i < lockDataFromJson.needItemIdxArray.Length; i++)
            {
                lst.Add(new LockInfo(lockDataFromJson.needItemIdxArray[i], lockDataFromJson.noItemString[i]));
            }
            LockDataList.Add(new LockData(lockDataFromJson.idx, lockDataFromJson.name, lockDataFromJson.firstInteractString, lst));
        }
        return;
    }

    public override void SaveJson()
    {
        foreach (LockData lockData in LockDataList){
            JObject itemJson = new JObject(
                new JProperty("idx", lockData.idx),
                new JProperty("name", lockData.name),
                new JProperty("itemInfo", lockData.interactString),
                new JProperty("lockInfoList", lockData.lockInfoList)
            );
        }
    }
}
[CustomEditor(typeof(LockDataBase))]
public class LockDataBaseEditor : DataBaseEditor<LockDataBase>{ }