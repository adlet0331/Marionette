using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

/* 게임 내 Script를 가지는 오브젝트의 데이터 List
 * 
 * ScriptableObjectData의 List
 * 데이터베이스 비슷하게 사용
 * 
 */
[Serializable]
public class ScriptObjDataStruct
{
    public int idx;
    public string name;
    public string scriptableScript;
    public string[] scripts;
}
public class ScriptObjDataManager : Singleton<ScriptObjDataManager>
{
    public List<ScriptableObjData> ScriptObjDataList;
    private void Start()
    {
        LoadJson();
    }

    public void LoadJson()
    {
        ScriptObjDataList = new List<ScriptableObjData>();

        TextAsset jsonData = Resources.Load<TextAsset>("IngameData/Scripts");
        ScriptObjDataStruct[] Datas = JsonHelper.FromJson<ScriptObjDataStruct>("{\"resources\":" + jsonData.text + "}");
        foreach (ScriptObjDataStruct scriptDataStruct in Datas)
        {
            ScriptObjDataList.Add(new ScriptableObjData(scriptDataStruct.idx, scriptDataStruct.name, scriptDataStruct.scriptableScript, scriptDataStruct.scripts.ToList()));
        }
        return;
    }
}
